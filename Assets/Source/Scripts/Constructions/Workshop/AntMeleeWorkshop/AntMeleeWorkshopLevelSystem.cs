using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntMeleeWorkshopLevelSystem : ConstructionLevelSystemBase<AntMeleeWorkshopLevel>
    {
        public AntMeleeWorkshopLevelSystem(ConstructionBase construction, AntMeleeWorkshopConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            
        }
    }
}