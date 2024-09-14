using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionBuyCostConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ResourceID, int> buyCost;
        
        public IReadOnlyDictionary<ResourceID, int> BuyCost => buyCost;
    }
}