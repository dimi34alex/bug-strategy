using BugStrategy.ScenesLoading;
using BugStrategy.Tiles.WarFog;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorBootstrap : MonoBehaviour
    {
        [Inject] private TileFogVisibilityModificator _tileFogVisibilityModificator;
        [Inject] private ISceneLoader _sceneLoader;
        
        private void Start()
        {
            _tileFogVisibilityModificator.SetState(false);
            _sceneLoader.Initialize(false);
        }
    }
}