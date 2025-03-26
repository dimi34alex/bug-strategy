using BugStrategy.Grids;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstructionsGrid : GridRepository<EditorConstruction>
    {
        public EditorConstructionsGrid(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}