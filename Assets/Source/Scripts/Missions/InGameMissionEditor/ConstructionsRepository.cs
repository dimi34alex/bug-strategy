using BugStrategy.Constructions;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class ConstructionsRepository : GridRepository<ConstructionBase>
    {
        public ConstructionsRepository(GridConfig gridConfig) : base(gridConfig) { }
    }
}