using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.ResourceProduceConstruction.AntAphidFarm
{
    [Serializable]
    public class AntAphidFarmLevelSystem : ConstructionLevelSystemBase<AntAphidFarmLevel>
    {
        private readonly ResourceProduceCore _resourceProduceCore;
        
        public AntAphidFarmLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntAphidFarmConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, ref ResourceProduceCore resourceProduceCore, 
            FloatStorage healthStorage) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
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