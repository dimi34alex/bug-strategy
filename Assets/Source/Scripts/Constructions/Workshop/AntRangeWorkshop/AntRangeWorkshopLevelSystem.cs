using System;
using Constructions.LevelSystemCore;
using Source.Scripts;
using Source.Scripts.ResourcesSystem;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Constructions
{
    [Serializable]
    public class AntRangeWorkshopLevelSystem : ConstructionLevelSystemBase<AntRangeWorkshopLevel>
    {
        public AntRangeWorkshopLevelSystem(ConstructionBase construction, AntRangeWorkshopConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            
        }
    }
}