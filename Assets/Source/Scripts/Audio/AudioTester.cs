using BugStrategy.Audio.Sources;
using BugStrategy.CustomInput;
using UnityEngine;
using Zenject;

namespace BugStrategy.Audio
{
    public class AudioTester : MonoBehaviour
    {
        [SerializeField] private AudioSourceHolderPoolable audioSourcePrefab;
        
        [Inject] private IInputProvider _inputProvider;
        [Inject] private AudioFactory _audioFactory;

        private void Update()
        {
            if (_inputProvider.LmbDown) 
                _audioFactory.Create(audioSourcePrefab, _inputProvider.MousePosition);
        }
    }
}