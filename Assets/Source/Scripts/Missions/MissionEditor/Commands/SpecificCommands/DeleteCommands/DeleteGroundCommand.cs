using BugStrategy.Grids;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteGroundCommand : DeleteCommandBase<int, Tile>
    {
        private readonly TilesFactory _factory;

        public DeleteGroundCommand(Vector3 point, TilesFactory factory, 
            GridRepository<Tile> positionsRepository) 
            : base(point, positionsRepository)
        {
            _factory = factory;
        }

        protected override Tile Create(int tileId, Vector3 point) 
            => _factory.Create(tileId, point, false);

        protected override int GetId(Tile tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;
    }
}