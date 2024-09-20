using System.Collections.Generic;
using BugStrategy.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class TilesBuilder : BuilderCell<Tile>
    {
        private readonly TilesFactory _tilesFactory;

        public TilesBuilder(GridConfig gridConfig, IReadOnlyList<Tile> config, GridRepository<Tile> gridRepository, 
            TilesFactory tilesFactory) 
            : base(gridConfig, config, gridRepository)
        {
            _tilesFactory = tilesFactory;
        }

        public override void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, Config.Count);
            _tilesFactory.Create(Config[randomIndex], GridConfig.RoundPositionToGrid(point), Quaternion.identity);
        }
    }
}