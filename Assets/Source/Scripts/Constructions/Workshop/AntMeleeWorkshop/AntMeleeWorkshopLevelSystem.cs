using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntMeleeWorkshop
{
    [Serializable]
    public class AntMeleeWorkshopLevelSystem : ConstructionLevelSystemBase<AntMeleeWorkshopLevel>
    {
        public AntMeleeWorkshopLevelSystem(ConstructionBase construction, AntMeleeWorkshopConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            
        }
    }
}