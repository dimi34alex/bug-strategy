using System;
using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BarrackLevel : ConstructionLevelBase
    {
        [SerializeField] [Range(1, 6)] private int recruitingSize = 1;
        [SerializeField] private List<UnitRecruitingData<BeesRecruitingID>> beesRecruitingData;

        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData<BeesRecruitingID>> BeesRecruitingData => beesRecruitingData;
    }
}