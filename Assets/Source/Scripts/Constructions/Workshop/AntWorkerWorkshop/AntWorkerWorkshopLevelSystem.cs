using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntWorkerWorkshop
{
    [Serializable]
    public class AntWorkerWorkshopLevelSystem : ConstructionLevelSystemBase<AntWorkerWorkshopLevel>
    {
        public AntWorkerWorkshopLevelSystem(ConstructionBase construction, AntWorkerWorkshopConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            
        }
    }
}