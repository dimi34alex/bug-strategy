using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class TilesBuilder : GridBuilder<int, Tile>
    {
        private readonly CommandsFactory _commandsFactory;
        private readonly IReadOnlyList<int> _keys;

        public TilesBuilder(GridConfig gridConfig, GridRepository<Tile> gridRepository, TilesFactory factory, 
            CommandsFactory commandsFactory) 
            : base(gridConfig, gridRepository, factory)
        {
            _commandsFactory = commandsFactory;
            _keys = factory.GetKeys();
        }

        protected override ICommand CreateCommand(int id, Vector3 point) 
            => _commandsFactory.BuildTileCommand(id, point);

        public void Generate(Vector2Int size) 
            => Generate(size.x, size.y);
        
        public async void Generate(int x, int y)
        {
            if (x % 2 == 0)
                x += 1;

            if (y % 2 == 0)
                y += 1;

            var newTiles = new Dictionary<Vector3, int>(x * y);
            
            var shortColumnsCount = 0;
            if ((x + 1) % 4 != 0) 
                shortColumnsCount = (int)Math.Ceiling((float)x / 2);
            else
                shortColumnsCount = (int)Math.Floor((float)x / 2);
            var longColumnsCount = x - shortColumnsCount;
            
            var center = GridConfig.RoundPositionToGrid(new Vector2(0, 0));
            
            var sxStartPoint = center.x - GridConfig.HexagonsOffsets.x * (int)Math.Floor((float)shortColumnsCount / 2);
            var syStartPoint = center.y + GridConfig.HexagonsOffsets.y * (y - 1);
            var shortColumnsStart = new Vector2(sxStartPoint, syStartPoint);

            var lxStartPoint = center.x - GridConfig.HexagonsOffsets.x / 2 - GridConfig.HexagonsOffsets.x * (int)Math.Floor((float)(longColumnsCount - 1) / 2);
            var lyStartPoint = center.y + GridConfig.HexagonsOffsets.y + GridConfig.HexagonsOffsets.y * (y - 1);
            var longColumnsStart = new Vector2(lxStartPoint, lyStartPoint);
            
            shortColumnsStart = GridConfig.RoundPositionToGrid(shortColumnsStart);
            longColumnsStart = GridConfig.RoundPositionToGrid(longColumnsStart);
            
            var columnStartPoint = shortColumnsStart;
            for (int i = 0; i < shortColumnsCount; i++)
            {
                await SpawnColumn(columnStartPoint, y, newTiles);
                columnStartPoint += Vector2.right * GridConfig.HexagonsOffsets.x;
            }

            columnStartPoint = longColumnsStart;
            for (int i = 0; i < longColumnsCount; i++)
            {
                await SpawnColumn(columnStartPoint, y + 1, newTiles);
                columnStartPoint += Vector2.right * GridConfig.HexagonsOffsets.x;
            }

            var command = _commandsFactory.GenerateGroundTilesCommand(newTiles);
            command.Execute();
        }
        
        private async Task SpawnColumn(Vector2 startPoint, int y, Dictionary<Vector3, int> newTiles)
        {
            var curPoint = startPoint;
            for (int i = 0; i < y; i++)
            {
                var spawnPoint = new Vector3(curPoint.x, 0, curPoint.y);
                newTiles.Add(spawnPoint, GetRandomTileId());
                curPoint += Vector2.down * GridConfig.HexagonsOffsets.y * 2;
                // await Task.Delay(100);
            }
        }
        
        private int GetRandomTileId()
        {
            var randomIndex = Random.Range(0, _keys.Count);
            return _keys[randomIndex];
        }
    }
}