using BugStrategy.Grids;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionsRepository : GridRepository<ConstructionBase>, IConstructionGrid
    {
        public ConstructionsRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
    }
}
