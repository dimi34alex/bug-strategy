using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;

namespace BugStrategy.Constructions.AntRangeWorkshop
{
    [Serializable]
    public class AntRangeWorkshopLevelSystem : ConstructionLevelSystemBase<AntRangeWorkshopLevel>
    {
        private readonly WorkshopCore _workshopCore;
        
        public AntRangeWorkshopLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntRangeWorkshopConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, WorkshopCore workshopCore) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _workshopCore = workshopCore;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            SetCapacityPerTool();
            SetRangAccess();
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            SetCapacityPerTool();
            SetRangAccess();
        }

        private void SetCapacityPerTool() 
            => _workshopCore.SetCapacity(CurrentLevel.CapacityPerTool);
        
        private void SetRangAccess() 
            => _workshopCore.SetRangAccess(CurrentLevel.RangAccess);
    }
}