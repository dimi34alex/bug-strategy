using System;
using System.Threading;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Missions.MissionEditor.Commands;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.Missions.MissionEditor.Saving;
using BugStrategy.Missions.MissionEditor.UI;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorBuilder : MonoBehaviour
    {
        [SerializeField] private MissionEditorConfig config;

        [Inject] private GridConfig _gridConfig;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private EditorConstructionsFactory _editorConstructionsFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;

        [Inject] private GroundPositionsRepository _groundPositionsRepository;
        [Inject] private EditorConstructionsRepository _editorConstructionsRepository;
        [Inject] private ResourceSourceRepository _resourceSourceRepository;
        
        [Inject] private CommandsRepository _commandsRepository;
        [Inject] private MissionEditorCommandsFactory _commandsFactory;
        
        private UI_MissionEditor _uiMissionEditor;
        
        private GroundBuilder _groundBuilder;
        private EditorConstructionsBuilder _editorConstructionsBuilder;
        private ResourceSourcesBuilder _resourceSourceBuilder;

        private IGridBuilder _activeBuilder;
        
        private void Awake()
        {
            _uiMissionEditor = FindObjectOfType<UI_MissionEditor>(true);

            _editorConstructionsRepository.SetExternalGrids(new IGridRepository[] { _resourceSourceRepository });
            _resourceSourceRepository.SetExternalGrids(new IGridRepository[] { _editorConstructionsRepository });
            
            _groundBuilder = new GroundBuilder(_gridConfig, _groundPositionsRepository, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, _editorConstructionsRepository, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, _resourceSourceRepository, _resourceSourceFactory, _commandsFactory);
            
            _uiMissionEditor.OnTilePressed += TilePrep;
            _uiMissionEditor.OnConstructionPressed += ConstrPrep;
            _uiMissionEditor.OnResourceSourcePressed += ResourceSourcePrep;

            _groundBuilder.Generate(config.DefaultGridSize);
            _commandsRepository.Clear();
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();
        
        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _groundBuilder.DeActivate();
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void TilePrep(int ind)
        {
            _activeBuilder = _groundBuilder;
            _groundBuilder.Activate(ind);
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.DeActivate();
        }

        private void ConstrPrep(ConstructionID id)
        {
            _activeBuilder = _editorConstructionsBuilder;
            _groundBuilder.DeActivate();
            _editorConstructionsBuilder.Activate(id);
            _resourceSourceBuilder.DeActivate();
        }
        
        public void Generate(Vector2Int size)
            => _groundBuilder.Generate(size);

        [ContextMenu("SAVE")]
        public void Save() 
            => MissionSaveAndLoader.Save(_groundPositionsRepository.Tiles, _resourceSourceRepository.Tiles);

        public void Save(string fileName) 
            => MissionSaveAndLoader.Save(fileName, _groundPositionsRepository.Tiles, _resourceSourceRepository.Tiles);

        private CancellationTokenSource _mapLoadingCancelToken;
        public async void Load(string fileName)
        {
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

        private void OnDestroy()
        {
            _mapLoadingCancelToken?.Cancel();
        }
    }
}