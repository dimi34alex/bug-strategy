using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BeeHouseLevelSystem : ConstructionLevelSystemBase<BeeHouseLevel>
    {
        public BeeHouseLevelSystem(ConstructionLevelSystemBase<BeeHouseLevel> constructionLevelSystemBase,
            ref ResourceStorage healthStorage) 
            : base(constructionLevelSystemBase, ref healthStorage)
        { }
    }
}