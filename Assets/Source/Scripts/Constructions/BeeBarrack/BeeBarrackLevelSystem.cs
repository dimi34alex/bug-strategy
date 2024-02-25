using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using UnitsRecruitingSystem;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeeBarrackLevelSystem : ConstructionLevelSystemBase<BeeBarrackLevel>
    {
        private UnitsRecruiter<BeesRecruitingID> _recruiter;

        public BeeBarrackLevelSystem(IReadOnlyList<BeeBarrackLevel> levels,
            Transform spawnPosition, ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage,
            ref UnitsRecruiter<BeesRecruitingID> recruiter) 
            : base(levels, ref resourceRepository, ref healthStorage)
        {
            _recruiter = recruiter = new UnitsRecruiter<BeesRecruitingID>(CurrentLevel.RecruitingSize, spawnPosition,
                CurrentLevel.BeesRecruitingData, ref resourceRepository);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.AddStacks(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
        }
    }
}