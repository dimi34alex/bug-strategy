using System;
using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class TownHallLevel : ConstructionLevelBase
    {
        [SerializeField] [Range(1, 2)] private int recruitingSize = 0;
        [SerializeField] private List<UnitRecruitingData<BeesRecruitingID>> beesRecruitingData;

        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData<BeesRecruitingID>> BeesRecruitingData => beesRecruitingData;
    }
}