using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.AntStorage
{
    [Serializable]
    public class AntStorageLevelSystem : ConstructionLevelSystemBase<AntStorageLevel>
    {
        public AntStorageLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}