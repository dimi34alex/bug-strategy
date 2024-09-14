using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHouse
{
    [Serializable]
    public class BeeHouseLevel : ConstructionLevelBase
    {
        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
    }
}