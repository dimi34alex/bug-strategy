using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.BeeWaxTower
{
    public class BeeWaxTowerLevelSystem : ConstructionLevelSystemBase<BeeWaxTowerLevel>
    {
        private readonly BeeWaxTowerAttackProcessor _attackProcessor; 
        
        public BeeWaxTowerLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, BeeWaxTowerConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, 
            BeeWaxTowerAttackProcessor attackProcessor) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _attackProcessor = attackProcessor;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _attackProcessor.SetData(CurrentLevel.ProjectilesCount, CurrentLevel.Cooldown, CurrentLevel.SpawnPause, 
                CurrentLevel.Damage, CurrentLevel.ProjectilesType);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();
            
            _attackProcessor.SetData(CurrentLevel.ProjectilesCount, CurrentLevel.Cooldown, CurrentLevel.SpawnPause, 
                CurrentLevel.Damage, CurrentLevel.ProjectilesType);
        }
    }
}