using System.Collections.Generic;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;
using TilesFactory = BugStrategy.Tiles.TilesFactory;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class TilesBuilder : GridBuilder<int, Tile>
    {
        private readonly IReadOnlyList<int> _keys;
        private readonly TilesFactory _factory;

        public TilesBuilder(GridConfig gridConfig, GridRepository<Tile> gridRepository, TilesFactory factory) 
            : base(gridConfig, gridRepository)
        {
            _factory = factory;
            _keys = _factory.GetKeys();
        }

        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _keys.Count);
            var id = _keys[randomIndex];
            var tile = Create(id, point);
            
            if (!GridRepository.TryAdd(point, tile)) 
                Debug.LogError($"Cant spawn tile: {point}");
        }

        protected override Tile Create(int id, Vector3 point = default) 
            => _factory.Create(id, GridConfig.RoundPositionToGrid(point), Quaternion.identity);
    }
}