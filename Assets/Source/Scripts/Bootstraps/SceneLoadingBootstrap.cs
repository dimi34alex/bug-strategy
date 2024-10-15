using BugStrategy.ScenesLoading;
using CycleFramework.Execute;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class SceneLoadingBootstrap : CycleInitializerBase
    {
        [Inject] private ISceneLoader _sceneLoader;
        
        protected override void OnInit()
        {
            base.OnInit();
            _sceneLoader.Initialize(false);
        }
    }
}