using System;
using BugStrategy.Audio.Sources;
using BugStrategy.CustomInput;
using UnityEngine;
using Zenject;

namespace BugStrategy.Audio
{
    public class AudioTester : MonoBehaviour
    {
        [Inject] private IInputProvider _inputProvider;
        [Inject] private AudioFactory _audioFactory;

        private void Update()
        {
            if (_inputProvider.LmbDown) 
                _audioFactory.Create(AudioSourceType.Example0, _inputProvider.MousePosition);
        }
    }
}