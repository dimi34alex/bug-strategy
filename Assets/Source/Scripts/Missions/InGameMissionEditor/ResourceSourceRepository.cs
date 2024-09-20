using BugStrategy.ResourceSources;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class ResourceSourceRepository : GridRepository<ResourceSourceBase>
    {
        public ResourceSourceRepository(GridConfig gridConfig) : base(gridConfig) { }
    }
}