using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.BeeStorage
{
    [Serializable]
    public class BeeStorageLevelSystem : ConstructionLevelSystemBase<BeeStorageLevel>
    {
        public BeeStorageLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, BeeStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}