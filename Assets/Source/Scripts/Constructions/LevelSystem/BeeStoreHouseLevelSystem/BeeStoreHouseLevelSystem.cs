using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BeeStoreHouseLevelSystem : ConstructionLevelSystemBase<BeeStoreHouseLevel>
    {
        public BeeStoreHouseLevelSystem(ConstructionLevelSystemBase<BeeStoreHouseLevel> buildingLevelSystemBase,
            ref ResourceStorage healPoints)
            : base(buildingLevelSystemBase, ref healPoints)
        { }
    }
}