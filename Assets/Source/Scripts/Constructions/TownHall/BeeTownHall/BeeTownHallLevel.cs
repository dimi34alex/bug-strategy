using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Libs;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.BeeTownHall
{
    [Serializable]
    public class BeeTownHallLevel : ConstructionLevelBase
    {
        [Space]
        [SerializeField] [Range(0, 2)] private int recruitingSize = 0;
        [SerializeField] private SerializableDictionary<UnitType, UnitRecruitingData> recruitingData;

        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public int RecruitingSize => recruitingSize;
        public IReadOnlyDictionary<UnitType, UnitRecruitingData> RecruitingData => recruitingData;
    }
}