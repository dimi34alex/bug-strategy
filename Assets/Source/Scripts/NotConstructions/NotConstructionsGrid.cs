using BugStrategy.Grids;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionsGrid : GridRepository<NotConstructionBase>
    {
        public NotConstructionsGrid(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}
