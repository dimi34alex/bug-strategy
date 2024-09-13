using System;
using Constructions.LevelSystemCore;
using Source.Scripts;
using Source.Scripts.ResourcesSystem;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Constructions
{
    [Serializable]
    public class AntAphidFarmLevelSystem : ConstructionLevelSystemBase<AntAphidFarmLevel>
    {
        private readonly ResourceProduceCore _resourceProduceCore;
        
        public AntAphidFarmLevelSystem(ConstructionBase construction, AntAphidFarmConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, ref ResourceProduceCore resourceProduceCore, 
            FloatStorage healthStorage) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
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