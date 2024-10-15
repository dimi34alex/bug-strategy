using BugStrategy.Factory;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteGroundCommand : DeleteCommandBase<int, Tile>
    {
        public DeleteGroundCommand(Vector3 point, ObjectsFactoryBase<int, Tile> factory, 
            GridRepository<Tile> positionsRepository) 
            : base(point, factory, positionsRepository)
        {
        }

        protected override int GetId(Tile tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;
    }
}