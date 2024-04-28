using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntWorkerWorkshopLevelSystem : ConstructionLevelSystemBase<AntWorkerWorkshopLevel>
    {
        public AntWorkerWorkshopLevelSystem(ConstructionBase construction, AntWorkerWorkshopConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            
        }
    }
}