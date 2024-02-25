using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    public class ConstructionBuyCostConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ResourceID, int> buyCost;
        
        public IReadOnlyDictionary<ResourceID, int> BuyCost => buyCost;
    }
}