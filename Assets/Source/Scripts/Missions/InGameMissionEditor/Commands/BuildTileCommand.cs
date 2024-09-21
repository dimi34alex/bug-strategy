using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class BuildTileCommand : BuildCommand<int, Tile>
    {
        public BuildTileCommand(int tileId, Vector3 point, TilesFactory factory, 
            TilesPositionsRepository positionsRepository) 
            : base(-1, tileId, point, factory, positionsRepository)
        {
        }

        protected override Tile Create(int id, Vector3 point)
        {
            var tile = base.Create(id, point);
            tile.gameObject.AddComponent<EditorTileId>().Initialize(id);
            return tile;
        }

        protected override int GetId(Tile tile) 
            => tile.GetComponent<EditorTileId>().ID;

        protected override bool ReplacedIdIsValid(int replacedId) 
            => replacedId >= 0;
    }
}