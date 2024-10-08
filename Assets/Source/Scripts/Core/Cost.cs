﻿using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;

namespace BugStrategy
{
    public class Cost
    {
        private Dictionary<ResourceID, int> _resourceCost;

        public Dictionary<ResourceID, int> ResourceCost => _resourceCost;

        public Cost(KeyValuePair<ResourceID, int>[] keyValuePairs)
        {
            _resourceCost = new Dictionary<ResourceID, int>();

            foreach (var element in keyValuePairs)
                _resourceCost.Add(element.Key, element.Value);
        }

        public Cost(SerializableDictionary<ResourceID, int> keyValuePairs)
        {
            _resourceCost = new Dictionary<ResourceID, int>();

            foreach (var element in keyValuePairs)
                _resourceCost.Add(element.Key, element.Value);
        }
    
        public Cost(IReadOnlyDictionary<ResourceID, int> keyValuePairs)
        {
            _resourceCost = new Dictionary<ResourceID, int>();

            foreach (var element in keyValuePairs)
                _resourceCost.Add(element.Key, element.Value);
        }
    }
}