using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.ButterflyStorage
{
    [Serializable]
    public class ButterflyStorageLevelSystem : ConstructionLevelSystemBase<ButterflyStorageLevel>
    {
        public ButterflyStorageLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, ButterflyStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}