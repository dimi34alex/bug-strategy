using System.Collections.Generic;
using System.Text;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;

namespace BugStrategy
{
    public struct Cost
    {
        private readonly Dictionary<ResourceID, int> _resourceCost;

        public IReadOnlyDictionary<ResourceID, int> ResourceCost => _resourceCost;

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

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var resourceCost in _resourceCost) 
                str.AppendLine($"{resourceCost.Key}: {resourceCost.Value}");

            return str.ToString();
        }
    }
}