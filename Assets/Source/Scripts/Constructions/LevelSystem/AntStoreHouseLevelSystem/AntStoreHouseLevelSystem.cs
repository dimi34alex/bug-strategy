using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class AntStoreHouseLevelSystem : ConstructionLevelSystemBase<AntStoreHouseLevel>
    {
        public AntStoreHouseLevelSystem(ConstructionLevelSystemBase<AntStoreHouseLevel> buildingLevelSystemBase,
            ref ResourceStorage healPoints)
            : base(buildingLevelSystemBase, ref healPoints)
        { }
    }
}