using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.ButterflyStorage
{
    [Serializable]
    public class ButterflyStorageLevelSystem : ConstructionLevelSystemBase<ButterflyStorageLevel>
    {
        public ButterflyStorageLevelSystem(ConstructionBase construction, ButterflyStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}