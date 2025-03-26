using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionsRepository : GridRepository<ConstructionBase>, IConstructionGrid
    {
        public ConstructionsRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
        
        public bool Exist(Vector3 position, bool blockIgnore = true) 
            => base.Exist(position, blockIgnore);
    
        public bool Exist<TType>(Vector3 position, bool blockIgnore = true) where TType : IConstruction 
            => Exist(position, blockIgnore) && Get(position) is TType;
    }
}
