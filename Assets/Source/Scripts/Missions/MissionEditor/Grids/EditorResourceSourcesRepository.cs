using BugStrategy.Grids;
using BugStrategy.ResourceSources;

namespace BugStrategy.Missions.MissionEditor.Grids
{
    public class EditorResourceSourcesRepository : GridRepository<ResourceSourceBase>
    {
        public EditorResourceSourcesRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}