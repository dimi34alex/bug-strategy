using System.Collections.Generic;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class EvolveConstructionConfigBase<T> : ConstructionBuyCostConfig
        where T : ConstructionLevelBase
    {
        [field: SerializeField] private List<T> levels;

        public IReadOnlyList<T> Levels => levels;
    }
}