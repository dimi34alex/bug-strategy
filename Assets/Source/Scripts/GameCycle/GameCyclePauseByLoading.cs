using System;
using BugStrategy.ScenesLoading;
using CycleFramework.Bootload;

namespace BugStrategy.GameCycle
{
    public class GameCyclePauseByLoading : IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        
        public GameCyclePauseByLoading(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.OnLoadingStarted += SwitchState;
        }

        private static void SwitchState() 
            => FrameworkCommander.SetFrameworkState(CycleState.Pause);

        public void Dispose() 
            => _sceneLoader.OnLoadingStarted += SwitchState;
    }
}