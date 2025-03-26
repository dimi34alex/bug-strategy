using System;
using System.Threading;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.CustomInput;
using BugStrategy.Grids;
using BugStrategy.Missions.MissionEditor.Affiliation;
using BugStrategy.Missions.MissionEditor.Commands;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.Grids;
using BugStrategy.Missions.MissionEditor.Saving;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorBuilder : MonoBehaviour
    {
        [SerializeField] private MissionEditorConfig config;

        [Inject] private readonly IInputProvider _inputProvider;
        
        [Inject] private GridConfig _gridConfig;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private EditorConstructionsFactory _editorConstructionsFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;

        [Inject] private TilesRepository _groundPositionsRepository;
        [Inject] private EditorConstructionsGrid _editorConstructionsGrid;
        [Inject] private EditorResourceSourcesRepository _resourceSourceRepository;

        [Inject] private AffiliationHolder _affiliationHolder;
        [Inject] private CommandsRepository _commandsRepository;
        [Inject] private MissionEditorCommandsFactory _commandsFactory;
        
        private GroundBuilder _groundBuilder;
        private EditorConstructionsBuilder _editorConstructionsBuilder;
        private ResourceSourcesBuilder _resourceSourceBuilder;
        private IGridBuilder _activeBuilder;
        private CancellationTokenSource _mapLoadingCancelToken;

        private void Awake()
        {
            _editorConstructionsGrid.SetExternalGrids(new IGridRepository[] { _resourceSourceRepository });
            _resourceSourceRepository.SetExternalGrids(new IGridRepository[] { _editorConstructionsGrid });
            
            _groundBuilder = new GroundBuilder(_gridConfig, _groundPositionsRepository, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, _editorConstructionsGrid, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, _resourceSourceRepository, _resourceSourceFactory, _commandsFactory);
            
            _groundBuilder.Generate(config.DefaultGridSize);
            _commandsRepository.Clear();
        }
        
        private void OnDestroy()
        {
            _mapLoadingCancelToken?.Cancel();
        }

        private void Update()
        {
            if (_inputProvider.RmbDown)
            {
                if (_activeBuilder == null || !_activeBuilder.IsActive)
                {
                    var point = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
                    point.y = 0;
                    
                    _editorConstructionsBuilder.Clear(point);
                    _resourceSourceBuilder.Clear(point);
                }
                else
                    _activeBuilder?.DeActivate();
            }
            
            if (_inputProvider.MouseCursorOverUi())
                return;

            var worldPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            worldPoint.y = 0;
            _activeBuilder?.Move(worldPoint);
            
            
            if (_inputProvider.LmbDown)
                _activeBuilder?.ApplyBuild();
        }

        public void ActivateGroundTile(int ind)
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            worldPoint.y = 0;
            
            _activeBuilder?.DeActivate();
            _activeBuilder = _groundBuilder;
            _groundBuilder.Activate(ind, worldPoint);
        }

        public void ActivateConstructions(ConstructionID id)
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            worldPoint.y = 0;
            
            _activeBuilder?.DeActivate();
            _activeBuilder = _editorConstructionsBuilder;
            _editorConstructionsBuilder.Activate((id, _affiliationHolder.PlayerAffiliation), worldPoint);
        }
        
        public void ActivateResourceSource(int index)
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            worldPoint.y = 0;
            
            _activeBuilder?.DeActivate();
            _activeBuilder = _resourceSourceBuilder;
            _resourceSourceBuilder.Activate(index, worldPoint);
        }

        public void Generate(Vector2Int size)
            => _groundBuilder.Generate(size);

        public void Save() 
            => MissionSaveAndLoader.Save(_groundPositionsRepository.Tiles, _editorConstructionsGrid.Tiles,  _resourceSourceRepository.Tiles);

        public void Save(string fileName) 
            => MissionSaveAndLoader.Save(fileName, _groundPositionsRepository.Tiles, _editorConstructionsGrid.Tiles, _resourceSourceRepository.Tiles);

        public async void Load(string fileName)
        {
            if (_mapLoadingCancelToken != null)
            {
                Debug.LogError("You cant start loading of mission while you load mission");
                return;
            }

            using (_mapLoadingCancelToken = new CancellationTokenSource())
            {
                try
                {
                    await MissionSaveAndLoader.Load(_mapLoadingCancelToken.Token, fileName, _groundBuilder,
                        _editorConstructionsBuilder, _resourceSourceBuilder);
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