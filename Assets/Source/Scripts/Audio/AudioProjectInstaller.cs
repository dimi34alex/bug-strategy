using BugStrategy.Audio.Ambience;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace BugStrategy.Audio
{
    public class AudioProjectInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer audioMixer;

        public override void InstallBindings()
        {
            BindAudioVolumeChanger();
            BindAmbience();
        }

        private void BindAudioVolumeChanger()
        {
            Container.Bind<AudioVolumeChanger>().FromNew().AsSingle()
                .WithArguments(audioMixer, new VolumeSettings()).NonLazy();
        }

        private void BindAmbience()
        {
            var ambienceManagerHolder = new GameObject() { name =  nameof(AmbienceManager)};
            var ambienceManager = ambienceManagerHolder.AddComponent<AmbienceManager>();

            Container.Bind<AmbienceManager>().FromInstance(ambienceManager).AsSingle();
        }
    }
}