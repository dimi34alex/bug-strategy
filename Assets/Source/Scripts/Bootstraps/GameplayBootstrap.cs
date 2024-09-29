using System;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Constructions.Factory;
using BugStrategy.Missions;
using BugStrategy.ResourceSources;
using BugStrategy.ScenesLoading;
using BugStrategy.Tiles;
using CycleFramework.Bootload;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Bootstraps
{
    public class GameplayBootstrap : CycleInitializerBase
    {
        [Inject] private GridConfig _gridConfig;
        [Inject] private MissionData _missionData;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;
        [Inject] private IConstructionFactory _constructionFactory;
        [Inject] private ISceneLoader _sceneLoader;

        private bool _isDestroyed;
        private CancellationTokenSource _mapLoadingCancelToken;
        
        protected override async void OnInit()
        {
            _sceneLoader.OnLoadingScreenHided += SwitchState;

            await LoadMap();
            
            if (_isDestroyed)
                return;

            _sceneLoader.Initialize(false);
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
            _mapLoadingCancelToken?.Cancel();
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
                    await MapLoader.LoadMap(_mapLoadingCancelToken.Token, _gridConfig, 
                        _missionData.MissionConfig, _tilesFactory,
                        _resourceSourceFactory, _missionData.ResourceSourcesRepository,
                        _constructionFactory, _missionData.ConstructionsRepository);
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