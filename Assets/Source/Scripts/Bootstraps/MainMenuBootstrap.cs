using BugStrategy.ScenesLoading;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class MainMenuBootstrap : CycleInitializerBase
    {
        [SerializeField] private bool hideLoadingScreenInstantly;
        
        [Inject] private readonly ISceneLoader _sceneLoader;

        protected override void OnStartInit()
        {
            base.OnStartInit();
            _sceneLoader.HideLoadScreen(hideLoadingScreenInstantly);
        }
    }
}