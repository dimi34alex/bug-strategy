using System;
using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using BugStrategy.TechnologiesSystem;
using UnityEngine;

namespace BugStrategy.Constructions.ConstructionLevelSystemCore
{
    [Serializable]
    public class ConstructionLevelBase
    {
        [Header("Main")] 
        [SerializeField, Tooltip("Can be null")] protected Sprite view;
        [SerializeField][Min(0)] protected int maxHealPoints;
        [SerializeField] protected List<TechnologyId> unlockedTechnologies;
        [SerializeField] private SerializableDictionary<ResourceID, int> levelUpCost;
        [SerializeField] private SerializableDictionary<ResourceID, int> resourceCapacity;
    
        public int MaxHealPoints => maxHealPoints;
        public Sprite View => view;
        public IReadOnlyList<TechnologyId> UnlockedTechnologies => unlockedTechnologies;
        public IReadOnlyDictionary<ResourceID, int> LevelUpCost => levelUpCost;
        public IReadOnlyDictionary<ResourceID, int> ResourceCapacity => resourceCapacity;
    }
}