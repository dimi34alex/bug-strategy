using BugStrategy.Audio;
using BugStrategy.ScenesLoading;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class AppBootstrap : CycleInitializerBase
    {
        [SerializeField] private int sceneIndexForLoadingAfterInitializations = 2;
        
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private AudioVolumeChanger _audioVolumeChanger;
        
        protected override void OnStartInit()
        {
            base.OnStartInit();
            
            _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations, true);
            _audioVolumeChanger.StartInit();
        }
    }
}