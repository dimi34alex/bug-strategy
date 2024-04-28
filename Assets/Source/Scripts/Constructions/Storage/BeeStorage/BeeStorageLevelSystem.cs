using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeeStorageLevelSystem : ConstructionLevelSystemBase<BeeStorageLevel>
    {
        public BeeStorageLevelSystem(ConstructionBase construction, BeeStorageConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage)
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        { }
    }
}