using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntStorage
{
    [Serializable]
    public class AntStorageLevelSystem : ConstructionLevelSystemBase<AntStorageLevel>
    {
        public AntStorageLevelSystem(ConstructionBase construction, AntStorageConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage)
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}