using BugStrategy.Tiles;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public class TilesPositionsRepository : GridRepository<Tile>
    {
        public TilesPositionsRepository(GridConfig gridConfig) : base(gridConfig) { }
    }
}