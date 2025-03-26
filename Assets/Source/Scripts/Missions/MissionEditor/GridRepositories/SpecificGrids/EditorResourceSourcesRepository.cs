using BugStrategy.ResourceSources;

namespace BugStrategy.Missions.MissionEditor.GridRepositories
{
    public class EditorResourceSourcesRepository : GridRepository<ResourceSourceBase>
    {
        public EditorResourceSourcesRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}