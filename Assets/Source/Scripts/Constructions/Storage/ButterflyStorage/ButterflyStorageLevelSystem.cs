using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class ButterflyStorageLevelSystem : ConstructionLevelSystemBase<ButterflyStorageLevel>
    {
        public ButterflyStorageLevelSystem(ConstructionBase construction, ButterflyStorageConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage)
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        { }
    }
}