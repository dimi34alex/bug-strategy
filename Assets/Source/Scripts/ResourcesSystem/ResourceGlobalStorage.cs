using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGlobalStorage : MonoBehaviour
{
    private static ResourceRepository _resourceRepository;
    public ResourceConfig[] resourceConfigs;
    
    void Awake()
    {
        _resourceRepository = new ResourceRepository(resourceConfigs);
        _resourceRepository.CreateResource(ResourceID.Pollen,0,0);
        _resourceRepository.CreateResource(ResourceID.Bees_Wax,0,0);
        _resourceRepository.CreateResource(ResourceID.Housing,0,0);
        _resourceRepository.CreateResource(ResourceID.Honey,0,0);
    }

    public static void ChangeCapacity(ResourceID resourceID, float capacity)
    {
        ResourceBase resourceBase = _resourceRepository.GetResource(resourceID);
        resourceBase.SetCapacity(resourceBase.Capacity + capacity);
    }

    public static void ChangeValue(ResourceID resourceID, float value)
    {
        ResourceBase resourceBase = _resourceRepository.GetResource(resourceID);
        resourceBase.ChangeValue(value);
    }
    
    public static ResourceBase GetResource(ResourceID resourceID)
    {
        return _resourceRepository.GetResource(resourceID);
    }
}
