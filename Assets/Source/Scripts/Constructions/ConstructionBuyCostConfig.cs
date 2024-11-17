using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionBuyCostConfig : ScriptableObject
    {
        [field: SerializeField] public float BuildDuration { get; private set; } = 4;
        [SerializeField] private SerializableDictionary<ResourceID, int> buyCost;
        
        public IReadOnlyDictionary<ResourceID, int> BuyCost => buyCost;
    }
}