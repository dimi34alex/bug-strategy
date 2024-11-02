using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntRangeWorkshop
{
    [Serializable]
    public class AntRangeWorkshopLevelSystem : ConstructionLevelSystemBase<AntRangeWorkshopLevel>
    {
        private readonly WorkshopCore _workshopCore;
        
        public AntRangeWorkshopLevelSystem(ConstructionBase construction, AntRangeWorkshopConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, WorkshopCore workshopCore) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _workshopCore = workshopCore;
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