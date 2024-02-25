using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntStoreHouseLevelSystem : ConstructionLevelSystemBase<AntStoreHouseLevel>
    {
        public AntStoreHouseLevelSystem(IReadOnlyList<AntStoreHouseLevel> levels, ref ResourceStorage healPoints)
            : base(levels, ref healPoints)
        { }
    }
}