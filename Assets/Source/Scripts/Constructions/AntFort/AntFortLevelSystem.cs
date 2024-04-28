using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntFortLevelSystem : ConstructionLevelSystemBase<AntFortLevel>
    {
        public AntFortLevelSystem(ConstructionBase construction, AntFortConfig config, 
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            
        }
    }
}