using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntFort
{
    [Serializable]
    public class AntFortLevelSystem : ConstructionLevelSystemBase<AntFortLevel>
    {
        public AntFortLevelSystem(ConstructionBase construction, AntFortConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            
        }
    }
}