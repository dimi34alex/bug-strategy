using BugStrategy.CameraMovement;
using BugStrategy.ScenesLoading;
using BugStrategy.Tiles.WarFog;
using CycleFramework.Bootload;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class MissionEditorBootstrap : CycleInitializerBase
    {
        [Inject] private TileFogVisibilityModificator _tileFogVisibilityModificator;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private CameraBounds _cameraBounds;

        protected override void OnStartInit()
        {
            _sceneLoader.OnLoadingScreenHided += SwitchState;

            _tileFogVisibilityModificator.SetState(false);
            _cameraBounds.SetBounds(new Vector3(1,1, 1) * -100, new Vector3(1,1, 1) * 100);
            
            _sceneLoader.HideLoadScreen(false);
        }
        
        private void SwitchState()
        {
            _sceneLoader.OnLoadingScreenHided -= SwitchState;
            FrameworkCommander.SetFrameworkState(CycleState.Gameplay);
        }
    }
}