using System.Collections.Generic;
using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class EvolveConstructionConfigBase<T> : ConstructionBuyCostConfig
        where T : ConstructionLevelBase
    {
        [field: SerializeField] private List<T> levels;

        public IReadOnlyList<T> Levels => levels;
    }
}