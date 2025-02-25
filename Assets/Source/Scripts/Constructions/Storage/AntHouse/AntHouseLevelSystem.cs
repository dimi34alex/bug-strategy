using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.AntHouse
{
    [Serializable]
    public class AntHouseLevelSystem : ConstructionLevelSystemBase<AntHouseLevel>
    {
        public AntHouseLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntHouseConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}