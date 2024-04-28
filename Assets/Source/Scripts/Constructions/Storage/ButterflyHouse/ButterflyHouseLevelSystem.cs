using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class ButterflyHouseLevelSystem : ConstructionLevelSystemBase<ButterflyHouseLevel>
    {
        public ButterflyHouseLevelSystem(ConstructionBase construction, ButterflyHouseConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        { }
    }
}