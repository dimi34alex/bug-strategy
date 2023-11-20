using System;
using UnitsRecruitingSystem;
using UnityEngine;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class TownHallLevelSystem : ConstructionLevelSystemBase<TownHallLevel>
    {
        private UnitsRecruiter<BeesRecruitingID> _recruiter;

        public TownHallLevelSystem(ConstructionLevelSystemBase<TownHallLevel> constructionLevelSystemBase,
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