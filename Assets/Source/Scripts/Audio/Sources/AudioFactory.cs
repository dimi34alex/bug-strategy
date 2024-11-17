using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy.Audio.Sources
{
    public class AudioFactory
    {
        private readonly Transform _parent;
        private readonly DiContainer _diContainer;
        private readonly AudioSourcesConfig _config;
        private readonly Pool.Pool<AudioSourceHolderPoolable, AudioSourceType> _pool;
        
        public event Action<AudioSourceHolderPoolable> OnCreate;

        public AudioFactory(DiContainer diContainer, AudioSourcesConfig config)
        {
            _diContainer = diContainer;
            _config = config;

            _pool = new Pool.Pool<AudioSourceHolderPoolable, AudioSourceType>(InstantiateEntity);
            _parent = new GameObject { transform = { name = "Audio" } }.transform;
        }
        
        public void Create(AudioSourceType audioSourceType, Vector3 position = new())
        {
            var audioSource = _pool.ExtractElement(audioSourceType);
            
            audioSource.transform.position = position;

            var pitch = _config.Data[audioSourceType].Pitch;
            var pitchRange = _config.Data[audioSourceType].PitchRange;
            pitch += Random.Range(-pitchRange, pitchRange);
            
            audioSource.SetPitch(pitch);
            
            OnCreate?.Invoke(audioSource);
        }

        private AudioSourceHolderPoolable InstantiateEntity(AudioSourceType audioSourceType)
        {
            if (!_config.Data.ContainsKey(audioSourceType))
                throw new ArgumentOutOfRangeException($"{nameof(_config)} doesnt contains {audioSourceType}");

            var someAudioSource = _diContainer.InstantiatePrefab(_config.Data[audioSourceType].AudioSourceHolderPoolablePrefab, _parent).GetComponent<AudioSourceHolderPoolable>();
            return someAudioSource;
        }
    }
}