using System;
using System.Collections.Generic;
using UnityEngine;

namespace Constructions.LevelSystemCore
{
    [Serializable]
    public class ConstructionLevelBase
    {
        [Header("Main")]
        [SerializeField][Range(0F, 100000F)] protected int maxHealPoints = 0;
        [field: SerializeField] private SerializableDictionary<ResourceID, int> levelUpCost;
        [field: SerializeField] private SerializableDictionary<ResourceID, int> resourceCapacity;
    
        public int MaxHealPoints => maxHealPoints;
        public IReadOnlyDictionary<ResourceID, int> LevelUpCost => levelUpCost;
        public IReadOnlyDictionary<ResourceID, int> ResourceCapacity => resourceCapacity;
    }
}