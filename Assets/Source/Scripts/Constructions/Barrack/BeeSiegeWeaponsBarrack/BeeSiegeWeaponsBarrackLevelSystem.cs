using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsRecruitingSystemCore;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeeSiegeWeaponsBarrackLevelSystem : ConstructionLevelSystemBase<BeeSiegeWeaponsBarrackLevel>
    {
        private readonly UnitsRecruiter _recruiter;

        public BeeSiegeWeaponsBarrackLevelSystem(IReadOnlyList<BeeSiegeWeaponsBarrackLevel> levels, Transform spawnPosition, 
            UnitFactory unitFactory, ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage,
            ref UnitsRecruiter recruiter) 
            : base(levels, ref resourceRepository, ref healthStorage)
        {
            _recruiter = recruiter = new UnitsRecruiter(CurrentLevel.RecruitingSize, spawnPosition,
                CurrentLevel.RecruitingData, unitFactory, ref resourceRepository);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.AddStacks(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
        }
    }
}