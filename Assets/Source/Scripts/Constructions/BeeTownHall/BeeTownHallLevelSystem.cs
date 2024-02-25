using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using UnitsRecruitingSystem;
using UnityEngine;

namespace Constructions
{
    public class BeeTownHallLevelSystem : ConstructionLevelSystemBase<BeeTownHallLevel>
    {
        private UnitsRecruiter<BeesRecruitingID> _recruiter;

        public BeeTownHallLevelSystem(IReadOnlyList<BeeTownHallLevel> levels, Transform spawnPosition,
            ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage, 
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