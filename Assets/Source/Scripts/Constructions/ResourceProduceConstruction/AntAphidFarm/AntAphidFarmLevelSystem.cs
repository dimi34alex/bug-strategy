using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class AntAphidFarmLevelSystem : ConstructionLevelSystemBase<AntAphidFarmLevel>
    {
        private readonly ResourceProduceCore _resourceProduceCore;
        
        public AntAphidFarmLevelSystem(ConstructionBase construction, AntAphidFarmConfig config,
            IResourceGlobalStorage resourceGlobalStorage, ref ResourceProduceCore resourceProduceCore, 
            ResourceStorage healthStorage) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            _resourceProduceCore = resourceProduceCore = new ResourceProduceCore(CurrentLevel.ResourceProduceProcessInfo);
        }
        
        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _resourceProduceCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceProduceProcessInfo);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _resourceProduceCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceProduceProcessInfo);
        }
    }
}