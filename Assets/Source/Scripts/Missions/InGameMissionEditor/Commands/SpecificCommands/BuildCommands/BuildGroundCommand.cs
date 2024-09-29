using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class BuildGroundCommand : BuildCommand<int, Tile>
    {
        public BuildGroundCommand(int tileId, Vector3 point, TilesFactory factory, 
            GroundPositionsRepository positionsRepository) 
            : base(-1, tileId, point, factory, positionsRepository)
        {
        }

        protected override Tile Create(int id, Vector3 point)
        {
            var tile = base.Create(id, point);
            tile.gameObject.AddComponent<MissionEditorTileId>().Initialize(id);
            return tile;
        }

        protected override int GetId(Tile tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;

        protected override bool ReplacedIdIsValid(int replacedId) 
            => replacedId >= 0;
    }
}