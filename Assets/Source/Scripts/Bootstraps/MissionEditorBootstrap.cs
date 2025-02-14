using BugStrategy.CameraMovement;
using BugStrategy.ScenesLoading;
using BugStrategy.Tiles.WarFog;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class MissionEditorBootstrap : MonoBehaviour
    {
        [Inject] private TileFogVisibilityModificator _tileFogVisibilityModificator;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private CameraBounds _cameraBounds;
        
        private void Start()
        {
            _tileFogVisibilityModificator.SetState(false);
            _cameraBounds.SetBounds(new Vector3(1,1, 1) * -100, new Vector3(1,1, 1) * 100);
            
            _sceneLoader.HideLoadScreen(false);
        }
    }
}