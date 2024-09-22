using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Missions.InGameMissionEditor.UI;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionEditorBuilder : MonoBehaviour
    {
        [SerializeField] private Vector2Int initSize;
        [SerializeField] private MissionEditorConfig config;

        [Inject] private GridConfig _gridConfig;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private EditorConstructionsFactory _editorConstructionsFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;
        
        private MissionEditorUI _missionEditorUI;
        
        private TilesBuilder _tilesBuilder;
        private EditorConstructionsBuilder _editorConstructionsBuilder;
        private ResourceSourcesBuilder _resourceSourceBuilder;

        private IGridBuilder _activeBuilder;

        private CommandsFactory _commandsFactory;
        private CommandsRepository _commandsRepository;
        
        private void Awake()
        {
            _missionEditorUI = FindObjectOfType<MissionEditorUI>();

            var tilesRep = new TilesPositionsRepository();
            var constrRep = new EditorConstructionsRepository();
            var resRep = new ResourceSourceRepository();

            constrRep.SetGridBlocker(new IGridRepository[] { resRep });
            resRep.SetGridBlocker(new IGridRepository[] { constrRep });

            _commandsFactory = new CommandsFactory(_tilesFactory, tilesRep, _editorConstructionsFactory, constrRep,
                _resourceSourceFactory, resRep);
            _commandsRepository = new CommandsRepository(_commandsFactory);
            
            _tilesBuilder = new TilesBuilder(_gridConfig, tilesRep, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, constrRep, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, resRep, _resourceSourceFactory, _commandsFactory);
            
            _missionEditorUI.OnTilePressed += TilePrep;
            _missionEditorUI.OnConstructionPressed += ConstrPrep;
            _missionEditorUI.OnResourceSourcePressed += ResourceSourcePrep;

            _tilesBuilder.Generate(config.DefaultGridSize);
            _commandsRepository.Clear();
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();
        
        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _tilesBuilder.DeActivate();
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void TilePrep(int ind)
        {
            _activeBuilder = _tilesBuilder;
            _tilesBuilder.Activate(ind);
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.DeActivate();
        }

        private void ConstrPrep(ConstructionID id)
        {
            _activeBuilder = _editorConstructionsBuilder;
            _tilesBuilder.DeActivate();
            _editorConstructionsBuilder.Activate(id);
            _resourceSourceBuilder.DeActivate();
        }

        [ContextMenu("UndoLastCommand")]
        private void Undo() 
            => _commandsRepository.UndoLastCommand();

        [ContextMenu("ExecuteLastUndoCommand")]
        private void Ex() 
            => _commandsRepository.ExecuteLastUndoCommand();
        
        public void Generate(Vector2Int size)
            => _tilesBuilder.Generate(size);
    }
}