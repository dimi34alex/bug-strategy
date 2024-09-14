using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntHouse
{
    [Serializable]
    public class AntHouseLevelSystem : ConstructionLevelSystemBase<AntHouseLevel>
    {
        public AntHouseLevelSystem(ConstructionBase construction, AntHouseConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        { }
    }
}