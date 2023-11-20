using System;
using UnitsRecruitingSystem;
using UnityEngine;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BarrackLevelSystem : ConstructionLevelSystemBase<BarrackLevel>
    {
        private UnitsRecruiter<BeesRecruitingID> _recruiter;

        public BarrackLevelSystem(ConstructionLevelSystemBase<BarrackLevel> constructionLevelSystemBase,
            Transform spawnPosition, ref ResourceStorage healthStorage,
            ref UnitsRecruiter<BeesRecruitingID> recruiter) : base(constructionLevelSystemBase, ref healthStorage)
        {
            _recruiter = recruiter = new UnitsRecruiter<BeesRecruitingID>(
                constructionLevelSystemBase.CurrentLevel.RecruitingSize,
                spawnPosition,
                constructionLevelSystemBase.CurrentLevel.BeesRecruitingData);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.AddStacks(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
        }
    }
}