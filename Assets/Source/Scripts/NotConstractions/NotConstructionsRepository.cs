using BugStrategy.Missions.MissionEditor.GridRepositories;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionsRepository : GridRepository<NotConstructionBase>
    {
        public NotConstructionsRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}
