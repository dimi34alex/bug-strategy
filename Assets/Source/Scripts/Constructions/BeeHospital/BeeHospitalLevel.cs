using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHospital
{
    [Serializable]
    public class BeeHospitalLevel : ConstructionLevelBase
    {
        [field: SerializeField] public float HealPerSecond;
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
    }
}