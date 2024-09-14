using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.AntAphidFarm
{
    [Serializable]
    public class AntAphidFarmLevel : ConstructionLevelBase
    {
        [field: Space]
        [field: SerializeField] public int UnitsCount { get; private set; }
        [field: SerializeField] public ResourceProduceProccessInfo ResourceProduceProcessInfo { get; private set; }
    }
}