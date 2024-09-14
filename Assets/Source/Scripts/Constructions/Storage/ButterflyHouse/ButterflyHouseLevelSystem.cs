using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.ButterflyHouse
{
    [Serializable]
    public class ButterflyHouseLevelSystem : ConstructionLevelSystemBase<ButterflyHouseLevel>
    {
        public ButterflyHouseLevelSystem(ConstructionBase construction, ButterflyHouseConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}