using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class BuildGroundCommand : BuildCommand<int, Tile>
    {
        private readonly TilesFactory _factory;

        public BuildGroundCommand(int tileId, Vector3 point, TilesFactory factory, 
            TilesRepository positionsRepository) 
            : base(-1, tileId, point, positionsRepository)
        {
            _factory = factory;
        }

        protected override Tile Create(int id, Vector3 point)
        {
            var tile = _factory.Create(id, point, false);
            tile.gameObject.AddComponent<MissionEditorTileId>().Initialize(id);
            return tile;
        }

        protected override int GetId(Tile tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;

        protected override bool ReplacedIdIsValid(int replacedId) 
            => replacedId >= 0;
    }
}