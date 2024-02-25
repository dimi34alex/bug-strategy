using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using UnitsRecruitingSystem;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeeTownHallLevelSystem : ConstructionLevelSystemBase<BeeTownHallLevel>
    {
        private UnitsRecruiter<BeesRecruitingID> _recruiter;

        public BeeTownHallLevelSystem(IReadOnlyList<BeeTownHallLevel> levels, Transform spawnPosition, 
            ref ResourceStorage healthStorage, ref UnitsRecruiter<BeesRecruitingID> recruiter) 
            : base(levels, ref healthStorage)
        {
            _recruiter = recruiter = new UnitsRecruiter<BeesRecruitingID>(
                CurrentLevel.RecruitingSize,
                spawnPosition,
                CurrentLevel.BeesRecruitingData);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.AddStacks(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
        }
    }
}