using System;
using UnityEngine;

public class ResourceGlobalStorage : MonoBehaviour
{
    private static ResourceRepository _resourceRepository;
    public ResourceConfig[] resourceConfigs;

    public static event Action ResourceChanged;

    private void Awake()
    {
        _resourceRepository = new ResourceRepository(resourceConfigs);
        _resourceRepository.CreateResource(ResourceID.Pollen,10, 10);
        _resourceRepository.CreateResource(ResourceID.Bees_Wax,10, 10);
        _resourceRepository.CreateResource(ResourceID.Housing,10, 10);
        _resourceRepository.CreateResource(ResourceID.Honey,10, 10);

        _resourceRepository.OnResourceAdd += OnResourceAdded;

        foreach (var element in _resourceRepository.Resources)
            element.Value.OnChange += OnResourceChanged;

    }

    private void Start()
    {
        OnResourceChanged();
    }

    private void OnResourceChanged()
    {
        ResourceChanged?.Invoke();
    }

    private void OnResourceAdded(ResourceBase resourceBase)
    {
        ResourceChanged?.Invoke();
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
