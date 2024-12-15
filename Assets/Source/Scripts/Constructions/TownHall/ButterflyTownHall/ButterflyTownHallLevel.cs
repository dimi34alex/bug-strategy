using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyTownHall
{
    [Serializable]
    public class ButterflyTownHallLevel : ConstructionLevelBase
    {
        [Space]
        [SerializeField] [Range(0, 2)] private int recruitingSize = 0;
        [SerializeField] private List<UnitRecruitingData> butterfliesRecruitingData;

        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData> ButterfliesRecruitingData => butterfliesRecruitingData;
    }
}