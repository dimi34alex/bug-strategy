using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntStorageLevelSystem : ConstructionLevelSystemBase<AntStorageLevel>
    {
        public AntStorageLevelSystem(ConstructionBase construction, AntStorageConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage)
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        { }
    }
}