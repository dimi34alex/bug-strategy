using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BugStrategy.Audio.Sources
{
    public class AudioFactory
    {
        private readonly Transform _mainParent;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<string, Transform> _parents = new();
        private readonly Dictionary<string, Pool.Pool<AudioSourceHolderPoolable>> _pools = new();

        private AudioSourceHolderPoolable _hashedPrefab;
        
        public AudioFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _mainParent = new GameObject { transform = { name = "Audio" } }.transform;
        }

        public AudioSourceHolderPoolable Create(AudioSourceHolderPoolable prefab, Vector3 position)
        {
            var key = prefab.gameObject.name;
            var audioSource = Create(key, prefab, position);
            audioSource.AutoPitch();
            return audioSource;
        }
        
        public AudioSourceHolderPoolable Create(AudioSourceHolderPoolable prefab, Vector3 position, float pitch)
        {
            var key = prefab.gameObject.name;
            var audioSource = Create(key, prefab, position);
            audioSource.SetPitch(pitch);
            return audioSource;
        }
        
        public AudioSourceHolderPoolable Create(string key, AudioSourceHolderPoolable prefab, Vector3 position)
        {
            _hashedPrefab = prefab;
            if (!_pools.ContainsKey(key))
            {
                _pools.Add(key, new Pool.Pool<AudioSourceHolderPoolable>(InstantiateEntity));

                var parent = new GameObject { transform = { name = key , parent = _mainParent} }.transform;
                _parents.Add(key, parent);
            }
            
            var element = _pools[key].ExtractElement();
            element.transform.position = position;

            return element;
        }

        private AudioSourceHolderPoolable InstantiateEntity()
        {
            var someAudioSource = _diContainer.InstantiatePrefab(_hashedPrefab, _parents[_hashedPrefab.name]).GetComponent<AudioSourceHolderPoolable>();
            return someAudioSource;
        }
    }
}