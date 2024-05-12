using System;
using System.Collections.Generic;

public class TeamResourceRepository
{
    public ResourceRepository ResourceRepository { get; }

    public static event Action ResourceChanged;

    public TeamResourceRepository(ResourceConfig[] resourceConfigs, IReadOnlyDictionary<ResourceID, ResourceInitialState> initialStates)
    {
        ResourceRepository = new ResourceRepository(resourceConfigs);
            
        foreach (var resourcePair in initialStates)
            ResourceRepository.CreateResource(resourcePair.Key,
                resourcePair.Value.Value, resourcePair.Value.Capacity);
                
        ResourceRepository.OnResourceAdd += OnResourceAdded;
                
        foreach (var element in ResourceRepository.Resources)
            element.Value.OnChange += OnResourceChanged;
    }

    public void ChangeCapacity(ResourceID resourceID, float capacity)
    {
        ResourceBase resourceBase = ResourceRepository.GetResource(resourceID);
        resourceBase.SetCapacity(resourceBase.Capacity + capacity);
    }

    public void ChangeValue(ResourceID resourceID, float value)
    {
        ResourceBase resourceBase = ResourceRepository.GetResource(resourceID);
        resourceBase.ChangeValue(value);
    }
    
    public ResourceBase GetResource(ResourceID resourceID)
    {
        return ResourceRepository.GetResource(resourceID);
    }
        
    private void OnResourceChanged()
    {
        ResourceChanged?.Invoke();
    }

    private void OnResourceAdded(ResourceBase resourceBase)
    {
        ResourceChanged?.Invoke();
    }
}