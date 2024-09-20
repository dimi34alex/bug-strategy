using System;
using System.Threading.Tasks;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionBuilder : MonoBehaviour
    {
        [SerializeField] private Vector2Int initSize;
        [SerializeField] private MissionEditorConfig config;
        [SerializeField] private GridConfig gridConfig;

        [Inject] private TilesFactory _tilesFactory;
        
        private MissionEditorUI _missionEditorUI;
        
        private TilesPositionsRepository _tilesPositionsRepository;
        private ConstructionsRepository _constructionsRepository;
        
        private GameObject _activePrefab;
        private GameObject _activeTile;

        private BuilderCell<Tile> _tilesBuilder;
        private BuilderCell<ConstructionBase> _constructionsBuilder;
        private BuilderCell<ResourceSourceBase> _resourceSourceBuilder;

        private IBuilderCell _activeBuilder;
        
        private void Awake()
        {
            _missionEditorUI = FindObjectOfType<MissionEditorUI>();

            var tilesRep = new TilesPositionsRepository(gridConfig);
            var constrRep = new ConstructionsRepository(gridConfig);
            var resRep = new ResourceSourceRepository(gridConfig);
            
            _tilesBuilder = new TilesBuilder(gridConfig, config.Tiles, tilesRep, _tilesFactory);
            _constructionsBuilder = new BuilderCell<ConstructionBase>(gridConfig, config.Constructions, constrRep);
            _resourceSourceBuilder = new BuilderCell<ResourceSourceBase>(gridConfig, config.ResourceSources, resRep);
            
            _missionEditorUI.OnTilePressed += TilePrep;
            _missionEditorUI.OnConstructionPressed += ConstrPrep;
            _missionEditorUI.OnResourceSourcePressed += ResourceSourcePrep;

            InitialGenerate(initSize.x, initSize.y);
        }

        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _tilesBuilder.DeActivate();
            _constructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();

        private void TilePrep(int ind)
        {
            _activeBuilder = _tilesBuilder;
            _tilesBuilder.Activate(ind);
            _constructionsBuilder.DeActivate();
            _resourceSourceBuilder.DeActivate();
        }

        private void ConstrPrep(int ind)
        {
            _activeBuilder = _constructionsBuilder;
            _tilesBuilder.DeActivate();
            _constructionsBuilder.Activate(ind);
            _resourceSourceBuilder.DeActivate();
        }

        private async void InitialGenerate(int x, int y)
        {
            if (x % 2 == 0)
                x += 1;

            if (y % 2 == 0)
                y += 1;
            
            var shortLinesCount = 0;
            if (x - (x - 1) % 4 != 0) 
                shortLinesCount = (int)Math.Ceiling((float)x / 2);
            else
                shortLinesCount = (int)Math.Floor((float)x / 2);
            var longLinesCount = x - shortLinesCount;
            
            var center = gridConfig.RoundPositionToGrid(new Vector2(0, 0));
            
            var sxStartPoint = center.x - gridConfig.HexagonsOffsets.x * ((int)Math.Floor((float)shortLinesCount / 2));
            var syStartPoint = center.y + gridConfig.HexagonsOffsets.y * y / 2;
            var shortLinesStart = new Vector2(sxStartPoint, syStartPoint);

            var lxStartPoint = center.x - gridConfig.HexagonsOffsets.x * longLinesCount / 2;
            var lyStartPoint = center.y + gridConfig.HexagonsOffsets.y * y / 2 + gridConfig.HexagonsOffsets.y;
            var longLinesStart = new Vector2(lxStartPoint, lyStartPoint);
            
            shortLinesStart = gridConfig.RoundPositionToGrid(shortLinesStart);
            longLinesStart = gridConfig.RoundPositionToGrid(longLinesStart);
            
            Debug.Log($"{shortLinesCount} {longLinesCount}");
            
            var curPos = shortLinesStart;
            for (int i = 0; i < shortLinesCount; i++)
            {
                await SpawnVertLine(curPos, y);
                curPos += Vector2.right * gridConfig.HexagonsOffsets.x;
            }

            curPos = longLinesStart;
            for (int i = 0; i < longLinesCount; i++)
            {
                await SpawnVertLine(curPos, y + 1);
                curPos += Vector2.right * gridConfig.HexagonsOffsets.x;
            }
            
            // var center = gridConfig.RoundPositionToGrid(new Vector2(0, 0));
            // var xStartPoint = center.x - gridConfig.HexagonsOffsets.x * ((int)Math.Floor((float)x / 2));
            // var yStartPoint = center.y + gridConfig.HexagonsOffsets.y * y / 2;
            // var startPoint = new Vector2(xStartPoint, yStartPoint);
            // startPoint = gridConfig.RoundPositionToGrid(startPoint);
            // var curPos = startPoint;
            //
            // for (int i = 0; i < y; i++)
            // {
            //     await SpawnHorLine(curPos,  (int)Math.Floor((float)x / 2));
            //     curPos += Vector2.down * gridConfig.HexagonsOffsets.y * 2;
            // }
            //
            // curPos = startPoint + Vector2.right * gridConfig.HexagonsOffsets.x / 2 + Vector2.up * gridConfig.HexagonsOffsets.y;
            // for (int i = 0; i < x / 2; i++)
            // {
            //     await SpawnVertLine(curPos, y + 1);
            //     curPos += Vector2.right * gridConfig.HexagonsOffsets.x;
            // }
        }

        private async Task SpawnHorLine(Vector2 startPoint, int lenght)
        {
            var curPoint = startPoint;
            for (int i = 0; i < lenght; i++)
            {
                var spawnPoint = new Vector3(curPoint.x, 0, curPoint.y);
                _tilesBuilder.ManualRandomSpawn(spawnPoint);
                curPoint += Vector2.right * gridConfig.HexagonsOffsets.x;
                await Task.Delay(100);
            }
        }
        
        private async Task SpawnVertLine(Vector2 startPoint, int y)
        {
            var curPoint = startPoint;
            for (int i = 0; i < y; i++)
            {
                var spawnPoint = new Vector3(curPoint.x, 0, curPoint.y);
                _tilesBuilder.ManualRandomSpawn(spawnPoint);
                curPoint += Vector2.down * gridConfig.HexagonsOffsets.y * 2;
                await Task.Delay(100);
            }
        }
    }
}