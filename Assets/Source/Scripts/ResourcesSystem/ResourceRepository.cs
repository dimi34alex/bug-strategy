using System;
using System.Collections.Generic;

namespace BugStrategy.ResourcesSystem
{
    public class ResourceRepository
    {
        private readonly Dictionary<ResourceID, ResourceConfig> _configs;
        private readonly Dictionary<ResourceID, ResourceBase> _resources;

        public IReadOnlyDictionary<ResourceID, ResourceBase> Resources => _resources;
        public IReadOnlyDictionary<ResourceID, ResourceConfig> Configs => _configs;

        public event Action<ResourceBase> OnResourceAdd;
        public event Action ResourceChanged;
    
        public ResourceRepository(IReadOnlyList<ResourceConfig> resourceConfigs)
        {
            _configs = new Dictionary<ResourceID, ResourceConfig>(resourceConfigs.Count);
            foreach (var config in resourceConfigs) 
                _configs.Add(config.ID, config);
        
            _resources = new Dictionary<ResourceID, ResourceBase>(4);
        }
    
        public ResourceRepository(IReadOnlyList<ResourceConfig> resourceConfigs, 
            IReadOnlyDictionary<ResourceID, ResourceInitialState> initialStates)
        {
            _configs = new Dictionary<ResourceID, ResourceConfig>(resourceConfigs.Count);
            foreach (var config in resourceConfigs) 
                _configs.Add(config.ID, config);

            _resources = new Dictionary<ResourceID, ResourceBase>(initialStates.Count);
            foreach (var resourcePair in initialStates)
                CreateResource(resourcePair.Key, resourcePair.Value.Value, resourcePair.Value.Capacity);
        }

        public void CreateResource(ResourceID id, float currentValue, float capacity)
        {
            if (!_resources.ContainsKey(id))
            {
                var resource = new ResourceBase(_configs[id], currentValue, capacity);
                _resources.Add(id, resource);
                OnResourceAdd?.Invoke(resource);
                ResourceChanged?.Invoke();
            }
            else
            {
                throw new Exception($"Such a resource already exists: {id}");
            }
        }
    
        public void SetCapacity(ResourceID id, float newCapacity)
        {
            if (_resources.TryGetValue(id, out var resource))
            {
                resource.SetCapacity(newCapacity);
                ResourceChanged?.Invoke();
            }
            else
                throw new Exception($"No resource: {id}");
        }
    
        public void ChangeCapacity(ResourceID id, float changeValue)
        {
            if (_resources.TryGetValue(id, out var resource))
            {
                resource.SetCapacity(resource.Capacity + changeValue);
                ResourceChanged?.Invoke();
            }
            else
                throw new Exception($"No resource: {id}");
        }
    
        public void ChangeValue(ResourceID id, float changeValue)
        {
            if (_resources.TryGetValue(id, out var resource))
            {
                resource.ChangeValue(changeValue);
                ResourceChanged?.Invoke();
            }
            else
                throw new Exception($"No resource: {id}");
        }
    
        public IReadOnlyResource GetResource(ResourceID resourceType)
        {
            if (_resources.TryGetValue(resourceType, out var resource))
                return resource;
            else
                throw new Exception($"No resource: {resourceType}");
        }

        public bool CanBuy(Cost cost, int scale = 1) 
            => CanBuy(cost.ResourceCost, scale);

        public bool CanBuy(IReadOnlyDictionary<ResourceID, int> cost, int scale = 1)
        {
            foreach (var costCell in cost)
                if (costCell.Value * scale > GetResource(costCell.Key).CurrentValue)
                    return false;

            return true;
        }
        
        /// <summary>
        /// Before call it, check that repository have enough resources for this (<see cref="CanBuy"/>)
        /// </summary>
        public void ChangeValues(Cost cost, int scale = 1) 
            => ChangeValues(cost.ResourceCost, scale);

        /// <summary>
        /// Before call it, check that repository have enough resources for this (<see cref="CanBuy"/>)
        /// </summary>
        public void ChangeValues(IReadOnlyDictionary<ResourceID, int> cost, int scale = 1)
        {
            foreach (var element in cost) 
                ChangeValue(element.Key, element.Value * scale);
        }
    }
}
