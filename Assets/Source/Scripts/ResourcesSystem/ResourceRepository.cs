using System;
using System.Collections.Generic;

public class ResourceRepository
{
    private Dictionary<ResourceID, ResourceConfig> _configs;
    private Dictionary<ResourceID, ResourceBase> _resources;

    public Dictionary<ResourceID, ResourceBase> Resources => _resources;
    public Dictionary<ResourceID, ResourceConfig> Configs => _configs;

    public event Action<ResourceBase> OnResourceAdd;

    public ResourceRepository() { }

    public ResourceRepository(ResourceConfig[] resourceConfigs)
    {
        _configs = new Dictionary<ResourceID, ResourceConfig>(resourceConfigs.Length);
        foreach (ResourceConfig config in resourceConfigs)
        {
            _configs.Add(config.ID, config);
        }
        _resources = new Dictionary<ResourceID, ResourceBase>(3);
    }

    public void CreateResource(ResourceID id, float currentValue, float capacity)
    {
        if (!_resources.ContainsKey(id))
        {
            ResourceBase resource = new ResourceBase(_configs[id], currentValue, capacity);
            _resources.Add(id, resource);
            OnResourceAdd?.Invoke(resource);
        }
        else
        {
            throw new Exception("Such a resource already exists");
        }
    }

    public ResourceBase GetResource(ResourceID resourceType)
    {
        if (_resources.TryGetValue(resourceType, out ResourceBase resource))
            return resource;
        else
            throw new Exception("No resource");
    }
}
