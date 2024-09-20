using System.Threading.Tasks;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionBuilder : MonoBehaviour
    {
        [SerializeField] private MissionEditorConfig config;
        [SerializeField] private GridConfig gridConfig;

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
            
            _tilesBuilder = new BuilderCell<Tile>(gridConfig, config.Tiles, tilesRep);
            _constructionsBuilder = new BuilderCell<ConstructionBase>(gridConfig, config.Constructions, constrRep);
            _resourceSourceBuilder = new BuilderCell<ResourceSourceBase>(gridConfig, config.ResourceSources, resRep);
            
            _missionEditorUI.OnTilePressed += TilePrep;
            _missionEditorUI.OnConstructionPressed += ConstrPrep;
            _missionEditorUI.OnResourceSourcePressed += ResourceSourcePrep;

            InitialGenerate(5, 5);
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

            var center = gridConfig.RoundPositionToGrid(new Vector3(0, 0, 0));
            var startPoint = new Vector2(center.x - gridConfig.HexagonsOffsets.x * (int)(x / 2), center.y + gridConfig.HexagonsOffsets.y * (int)(y / 2));
            startPoint = gridConfig.RoundPositionToGrid(startPoint);
            var curPos = startPoint;
            
            for (int i = 0; i < y; i++)
            {
                await SpawnHorLine(curPos, x / 2 + 1);
                curPos += Vector2.down * gridConfig.HexagonsOffsets.y * 2;
            }

            curPos = startPoint + Vector2.right * gridConfig.HexagonsOffsets.x / 2 + Vector2.up * gridConfig.HexagonsOffsets.y;
            for (int i = 0; i < x / 2; i++)
            {
                await SpawnVertLine(curPos, y + 1);
                curPos += Vector2.right * gridConfig.HexagonsOffsets.x;
            }
        }

        private async Task SpawnHorLine(Vector2 startPoint, int lenght)
        {
            var curPoint = startPoint;
            for (int i = 0; i < lenght; i++)
            {
                var spawnPoint = new Vector3(curPoint.x, 0, curPoint.y);
                _tilesBuilder.ManualRandomSpawn(spawnPoint);
                curPoint += Vector2.right * gridConfig.HexagonsOffsets.x;
                await Task.Delay(500);
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
                await Task.Delay(500);
            }
        }
    }
}