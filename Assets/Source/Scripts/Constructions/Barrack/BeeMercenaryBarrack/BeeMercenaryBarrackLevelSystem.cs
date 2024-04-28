using System;
using Constructions.LevelSystemCore;
using UnitsHideCore;
using UnitsRecruitingSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeeMercenaryBarrackLevelSystem : ConstructionLevelSystemBase<BeeMercenaryBarrackLevel>
    {
        private readonly UnitsRecruiter _recruiter;
        private readonly UnitsHider _hider;

        public BeeMercenaryBarrackLevelSystem(ConstructionBase construction, BeeMercenaryBarrackConfig config, 
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage, UnitsRecruiter recruiter,
            UnitsHider hider) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            _recruiter = recruiter;
            _hider = hider;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}