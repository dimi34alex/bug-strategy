using System;
using System.Threading.Tasks;
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

            var tilesRep = new TilesPositionsRepository(_gridConfig);
            var constrRep = new EditorConstructionsRepository(_gridConfig);
            var resRep = new ResourceSourceRepository(_gridConfig);

            var blockerForConstr = new GridBlockChecker(resRep);
            var blockerForRes = new GridBlockChecker(constrRep);
            
            constrRep.SetGridBlocker(blockerForConstr);
            resRep.SetGridBlocker(blockerForRes);

            _commandsFactory = new CommandsFactory(_tilesFactory, tilesRep, _editorConstructionsFactory, constrRep,
                _resourceSourceFactory, resRep);
            _commandsRepository = new CommandsRepository(_commandsFactory);
            
            _tilesBuilder = new TilesBuilder(_gridConfig, tilesRep, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, constrRep, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, resRep, _resourceSourceFactory, _commandsFactory);
            
            _missionEditorUI.OnTilePressed += TilePrep;
            _missionEditorUI.OnConstructionPressed += ConstrPrep;
            _missionEditorUI.OnResourceSourcePressed += ResourceSourcePrep;

            InitialGenerate(config.DefaultGridSize);
        }

        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _tilesBuilder.DeActivate();
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();

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
        
        [ContextMenu("Respawn")]
        private void Respawn()
        {
            _tilesBuilder.Clear();
            InitialGenerate(initSize.x, initSize.y);
        }
        
        private void InitialGenerate(Vector2Int size) 
            => InitialGenerate(size.x, size.y);
        
        private async void InitialGenerate(int x, int y)
        {
            if (x % 2 == 0)
                x += 1;

            if (y % 2 == 0)
                y += 1;
            
            var shortColumnsCount = 0;
            if ((x + 1) % 4 != 0) 
                shortColumnsCount = (int)Math.Ceiling((float)x / 2);
            else
                shortColumnsCount = (int)Math.Floor((float)x / 2);
            var longColumnsCount = x - shortColumnsCount;
            
            var center = _gridConfig.RoundPositionToGrid(new Vector2(0, 0));
            
            var sxStartPoint = center.x - _gridConfig.HexagonsOffsets.x * (int)Math.Floor((float)shortColumnsCount / 2);
            var syStartPoint = center.y + _gridConfig.HexagonsOffsets.y * (y - 1);
            var shortColumnsStart = new Vector2(sxStartPoint, syStartPoint);

            var lxStartPoint = center.x - _gridConfig.HexagonsOffsets.x / 2 - _gridConfig.HexagonsOffsets.x * (int)Math.Floor((float)(longColumnsCount - 1) / 2);
            var lyStartPoint = center.y + _gridConfig.HexagonsOffsets.y + _gridConfig.HexagonsOffsets.y * (y - 1);
            var longColumnsStart = new Vector2(lxStartPoint, lyStartPoint);
            
            shortColumnsStart = _gridConfig.RoundPositionToGrid(shortColumnsStart);
            longColumnsStart = _gridConfig.RoundPositionToGrid(longColumnsStart);
            
            var columnStartPoint = shortColumnsStart;
            for (int i = 0; i < shortColumnsCount; i++)
            {
                await SpawnColumn(columnStartPoint, y);
                columnStartPoint += Vector2.right * _gridConfig.HexagonsOffsets.x;
            }

            columnStartPoint = longColumnsStart;
            for (int i = 0; i < longColumnsCount; i++)
            {
                await SpawnColumn(columnStartPoint, y + 1);
                columnStartPoint += Vector2.right * _gridConfig.HexagonsOffsets.x;
            }
        }
        
        private async Task SpawnColumn(Vector2 startPoint, int y)
        {
            var curPoint = startPoint;
            for (int i = 0; i < y; i++)
            {
                var spawnPoint = new Vector3(curPoint.x, 0, curPoint.y);
                _tilesBuilder.ManualRandomSpawn(spawnPoint);
                curPoint += Vector2.down * _gridConfig.HexagonsOffsets.y * 2;
                // await Task.Delay(100);
            }
        }
    }
}