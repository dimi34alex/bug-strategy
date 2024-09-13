using System;
using Constructions.LevelSystemCore;
using Source.Scripts;
using Source.Scripts.ResourcesSystem;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Constructions
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