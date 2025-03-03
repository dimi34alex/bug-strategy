using System;
using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Libs;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;
using UnityEngine;

namespace BugStrategy.Constructions.AntTownHall
{
    [Serializable]
    public class AntTownHallLevel : ConstructionLevelBase
    {
        [Space]
        [SerializeField] [Range(0, 2)] private int recruitingSize = 0;
        [SerializeField] private SerializableDictionary<UnitType, UnitRecruitingData> recruitingData;

        public int RecruitingSize => recruitingSize;
        public IReadOnlyDictionary<UnitType, UnitRecruitingData> RecruitingData => recruitingData;
    }
}