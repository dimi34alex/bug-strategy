﻿using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace BugStrategy.Audio
{
    public class AudioVolumeChangerInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer audioMixer;

        public override void InstallBindings()
        {
            Container.Bind<AudioVolumeChanger>().FromNew().AsSingle()
                .WithArguments(audioMixer, new VolumeSettings()).NonLazy();
        }
    }
}