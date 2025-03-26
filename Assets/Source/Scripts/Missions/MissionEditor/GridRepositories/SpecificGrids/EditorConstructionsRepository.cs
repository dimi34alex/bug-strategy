using BugStrategy.Missions.MissionEditor.EditorConstructions;

namespace BugStrategy.Missions.MissionEditor.GridRepositories
{
    public class EditorConstructionsRepository : GridRepository<EditorConstruction>
    {
        public EditorConstructionsRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}