using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Constructions.BeeBarrack
{
    [Serializable]
    public class BeeBarrackLevelSystem : ConstructionLevelSystemBase<BeeBarrackLevel>
    {
        private readonly UnitsRecruiter _recruiter;
        private readonly UnitsHider _hider;

        public BeeBarrackLevelSystem(ConstructionBase construction, BeeBarrackConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage,
            UnitsRecruiter recruiter, UnitsHider hider) 
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _recruiter = recruiter;
            _hider = hider;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}