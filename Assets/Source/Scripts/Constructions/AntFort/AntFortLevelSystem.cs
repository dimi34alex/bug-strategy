using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.AntFort
{
    [Serializable]
    public class AntFortLevelSystem : ConstructionLevelSystemBase<AntFortLevel>
    {
        public AntFortLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntFortConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            
        }
    }
}