using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.ButterflyHouse
{
    [Serializable]
    public class ButterflyHouseLevelSystem : ConstructionLevelSystemBase<ButterflyHouseLevel>
    {
        public ButterflyHouseLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, ButterflyHouseConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}