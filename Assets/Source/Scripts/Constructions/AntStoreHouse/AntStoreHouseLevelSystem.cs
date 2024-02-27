using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntStoreHouseLevelSystem : ConstructionLevelSystemBase<AntStoreHouseLevel>
    {
        public AntStoreHouseLevelSystem(IReadOnlyList<AntStoreHouseLevel> levels,
            ref ResourceRepository resourceRepository, ref ResourceStorage healPoints)
            : base(levels, ref resourceRepository, ref healPoints)
        { }
    }
}