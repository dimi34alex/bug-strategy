using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Constructions.AntMeleeWorkshop
{
    [Serializable]
    public class AntMeleeWorkshopLevelSystem : ConstructionLevelSystemBase<AntMeleeWorkshopLevel>
    {
        private readonly WorkshopCore _workshopCore;

        public AntMeleeWorkshopLevelSystem(ConstructionBase construction, AntMeleeWorkshopConfig config,
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