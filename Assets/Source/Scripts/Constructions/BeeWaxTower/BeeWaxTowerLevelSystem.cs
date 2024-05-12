using Constructions.LevelSystemCore;

namespace Constructions
{
    public class BeeWaxTowerLevelSystem : ConstructionLevelSystemBase<BeeWaxTowerLevel>
    {
        private readonly BeeWaxTowerAttackProcessor _attackProcessor; 
        
        public BeeWaxTowerLevelSystem(ConstructionBase construction, BeeWaxTowerConfig config, 
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage, 
            BeeWaxTowerAttackProcessor attackProcessor) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
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