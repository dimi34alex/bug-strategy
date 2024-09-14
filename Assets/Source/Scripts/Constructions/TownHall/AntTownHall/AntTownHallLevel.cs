using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.AntTownHall
{
    [Serializable]
    public class AntTownHallLevel : ConstructionLevelBase
    {
        [Space]
        [SerializeField] [Range(0, 2)] private int recruitingSize = 0;
        [SerializeField] private List<UnitRecruitingData> recruitingData;

        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData> RecruitingData => recruitingData;
    }
}