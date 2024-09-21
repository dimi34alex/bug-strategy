using System.Collections.Generic;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;
using TilesFactory = BugStrategy.Tiles.TilesFactory;

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

        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _keys.Count);
            var id = _keys[randomIndex];
            var tile = CreateMovableModel(id, point);
            
            if (!GridRepository.TryAdd(point, tile)) 
                Debug.LogError($"Cant spawn tile: {point}");
        }

        protected override ICommand CreateCommand(int id, Vector3 point) 
            => _commandsFactory.BuildTileCommand(id, point);
    }
}