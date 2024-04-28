using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntRangeWorkshopLevelSystem : ConstructionLevelSystemBase<AntRangeWorkshopLevel>
    {
        public AntRangeWorkshopLevelSystem(ConstructionBase construction, AntRangeWorkshopConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            
        }
    }
}