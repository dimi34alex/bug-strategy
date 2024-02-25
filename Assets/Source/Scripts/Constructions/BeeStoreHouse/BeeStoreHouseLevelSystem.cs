using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeeStoreHouseLevelSystem : ConstructionLevelSystemBase<BeeStoreHouseLevel>
    {
        public BeeStoreHouseLevelSystem(IReadOnlyList<BeeStoreHouseLevel> levels, ref ResourceStorage healPoints)
            : base(levels, ref healPoints)
        { }
    }
}