using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class ButterflyStoreHouseLevelSystem : ConstructionLevelSystemBase<ButterflyStoreHouseLevel>
    {
        public ButterflyStoreHouseLevelSystem(IReadOnlyList<ButterflyStoreHouseLevel> levels, 
            ref ResourceRepository resourceRepository, ref ResourceStorage healPoints)
            : base(levels, ref resourceRepository, ref healPoints)
        { }
    }
}