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
        
        protected override void OnInit()
        {
            base.OnInit();
            
            _sceneLoader.Initialize(false);
            _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations);
        }
    }
}