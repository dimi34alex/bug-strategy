using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class ButterflyStoreHouseLevelSystem : ConstructionLevelSystemBase<ButterflyStoreHouseLevel>
    {
        public ButterflyStoreHouseLevelSystem(ConstructionLevelSystemBase<ButterflyStoreHouseLevel> buildingLevelSystemBase,
            ref ResourceStorage healPoints)
            : base(buildingLevelSystemBase, ref healPoints)
        { }
    }
}