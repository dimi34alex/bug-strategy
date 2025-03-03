using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Libs;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.BeeBarrack
{
    [Serializable]
    public class BeeBarrackLevel : ConstructionLevelBase
    {
        [field: Space] 
        [SerializeField] [Range(1, 6)] private int recruitingSize = 1;
        [SerializeField] private SerializableDictionary<UnitType, UnitRecruitingData> recruitingData;
        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public int RecruitingSize => recruitingSize;
        public IReadOnlyDictionary<UnitType, UnitRecruitingData> RecruitingData => recruitingData;
    }
}