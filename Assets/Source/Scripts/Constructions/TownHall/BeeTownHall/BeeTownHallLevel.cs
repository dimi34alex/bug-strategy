using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using UnityEngine;
using UnitsRecruitingSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeeTownHallLevel : ConstructionLevelBase
    {
        [Space]
        [SerializeField] [Range(0, 2)] private int recruitingSize = 0;
        [SerializeField] private List<UnitRecruitingData> beesRecruitingData;

        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public int RecruitingSize => recruitingSize;
        public IReadOnlyList<UnitRecruitingData> BeesRecruitingData => beesRecruitingData;
    }
}