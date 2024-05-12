using System;
using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions.BeeHospital
{
    [Serializable]
    public class BeeHospitalLevel : ConstructionLevelBase
    {
        [field: SerializeField] public float HealPerSecond;
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
    }
}