using System;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.CameraMovement;
using BugStrategy.Constructions.Factory;
using BugStrategy.GameCycle;
using BugStrategy.GameField;
using BugStrategy.Missions;
using BugStrategy.ResourceSources;
using BugStrategy.ScenesLoading;
using BugStrategy.Tiles;
using CycleFramework.Bootload;
using CycleFramework.Execute;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class GameplayBootstrap : CycleInitializerBase
    {
        [SerializeField] private GameFieldConstructor fieldConstructor;

        [Inject] private readonly CameraBounds _cameraBounds;
        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly TilesFactory _tilesFactory;
        [Inject] private readonly ResourceSourceFactory _resourceSourceFactory;
        [Inject] private readonly IConstructionFactory _constructionFactory;
        [Inject] private readonly ISceneLoader _sceneLoader;

        private bool _isDestroyed;
        private CancellationTokenSource _mapLoadingCancelToken;
        private GameCyclePauseByLoading _gameCyclePauseByLoading;
        
        protected override async void OnStartInit()
        {
            _gameCyclePauseByLoading = new GameCyclePauseByLoading(_sceneLoader);
            _sceneLoader.OnLoadingScreenHided += SwitchState;

            await LoadMap();
            if (_isDestroyed)
                return;

            _sceneLoader.HideLoadScreen(false);
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
            _mapLoadingCancelToken?.Cancel();
            _gameCyclePauseByLoading.Dispose();
        }
        
        private void SwitchState()
        {
            _sceneLoader.OnLoadingScreenHided -= SwitchState;
            FrameworkCommander.SetFrameworkState(CycleState.Gameplay);
        }

        private async Task LoadMap()
        {
            using (_mapLoadingCancelToken = new CancellationTokenSource())
            {
                try
                {
                    var fieldSize = await MapLoader.LoadMap(_mapLoadingCancelToken.Token,
                        _missionData.MissionConfig, _tilesFactory,
                        _resourceSourceFactory, _missionData.ResourceSourcesRepository,
                        _constructionFactory, _missionData.ConstructionsRepository);

                    fieldConstructor.SetFieldSize(fieldSize);
                    _cameraBounds.SetBounds(-fieldSize.XoY(-100), fieldSize.XoY(100));
                }
                catch (OperationCanceledException e)
                {
                    Debug.Log($"Loading of map was canceled: {e}");
                }
                
                _mapLoadingCancelToken.Dispose();
                _mapLoadingCancelToken = null;
            }
        }
    }
}