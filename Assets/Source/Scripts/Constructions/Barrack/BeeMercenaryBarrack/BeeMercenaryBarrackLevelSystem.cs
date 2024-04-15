using System;
using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnitsRecruitingSystemCore;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeeMercenaryBarrackLevelSystem : ConstructionLevelSystemBase<BeeMercenaryBarrackLevel>
    {
        private readonly UnitsRecruiter _recruiter;
        private readonly UnitsHider _hider;

        public BeeMercenaryBarrackLevelSystem(BeeMercenaryBarrackConfig config, Transform spawnPosition, 
            UnitFactory unitFactory, ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage,
            ref UnitsRecruiter recruiter, ref UnitsHider hider) 
            : base(config.Levels, ref resourceRepository, ref healthStorage)
        {
            _recruiter = recruiter = new UnitsRecruiter(CurrentLevel.RecruitingSize, spawnPosition,
                CurrentLevel.RecruitingData, unitFactory, ref resourceRepository);
            
            _hider = hider = new UnitsHider(CurrentLevel.HiderCapacity ,unitFactory , spawnPosition, config.HiderAccess);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.AddStacks(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}