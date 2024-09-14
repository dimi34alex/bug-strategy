using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.BeeBarrack
{
    [Serializable]
    public class BeeBarrackLevel : ConstructionLevelBase
    {
        [SerializeField] [Range(1, 6)] private int recruitingSize = 1;
        [SerializeField] private List<UnitRecruitingData> beesRecruitingData;
        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData> BeesRecruitingData => beesRecruitingData;
    }
}