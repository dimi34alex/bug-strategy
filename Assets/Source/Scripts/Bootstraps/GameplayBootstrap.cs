using BugStrategy.ScenesLoading;
using CycleFramework.Bootload;
using CycleFramework.Execute;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class GameplayBootstrap : CycleInitializerBase
    {
        [Inject] private ISceneLoader _sceneLoader;
        
        protected override void OnInit()
        {
            _sceneLoader.OnLoadingScreenHided += SwitchState;
            _sceneLoader.Initialize(false);
        }

        private void SwitchState()
        {
            _sceneLoader.OnLoadingScreenHided -= SwitchState;
            FrameworkCommander.SetFrameworkState(CycleState.Gameplay);
        }
    }
}