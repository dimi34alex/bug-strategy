using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions
{
    [Serializable]
    public class AntWorkshopLevelBase : ConstructionLevelBase
    {
        [field: Space]
        [field: Tooltip("Mean max count of melee/range weapon or worker tools")]
        [field: SerializeField] public int Capacity { get; private set; }
    }
}