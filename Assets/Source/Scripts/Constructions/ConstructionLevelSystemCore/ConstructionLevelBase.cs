using System;
using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.Constructions.ConstructionLevelSystemCore
{
    [Serializable]
    public class ConstructionLevelBase
    {
        [Header("Main")] 
        [SerializeField, Tooltip("Can be null")] protected Sprite view;
        [SerializeField][Range(0F, 100000F)] protected int maxHealPoints = 0;
        [field: SerializeField] private SerializableDictionary<ResourceID, int> levelUpCost;
        [field: SerializeField] private SerializableDictionary<ResourceID, int> resourceCapacity;
    
        public int MaxHealPoints => maxHealPoints;
        public Sprite View => view;
        public IReadOnlyDictionary<ResourceID, int> LevelUpCost => levelUpCost;
        public IReadOnlyDictionary<ResourceID, int> ResourceCapacity => resourceCapacity;
    }
}