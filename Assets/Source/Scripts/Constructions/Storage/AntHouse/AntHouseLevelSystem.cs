using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntHouseLevelSystem : ConstructionLevelSystemBase<AntHouseLevel>
    {
        public AntHouseLevelSystem(ConstructionBase construction, AntHouseConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        { }
    }
}