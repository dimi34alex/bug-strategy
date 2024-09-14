using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.BeeStorage
{
    [Serializable]
    public class BeeStorageLevelSystem : ConstructionLevelSystemBase<BeeStorageLevel>
    {
        public BeeStorageLevelSystem(ConstructionBase construction, BeeStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}