using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInitializer : CycleInitializerBase
{
    [SerializeField] private ResourceConfig[] _resourceConfigs;
    private void Awake()
    {   
        ResourceRepository resourceRepository = FrameworkCommander.GlobalData.ResourceRepository;
        resourceRepository = new ResourceRepository(_resourceConfigs);
        resourceRepository.CreateResource(ResourceID.Bees_Wax,0,100);
        resourceRepository.CreateResource(ResourceID.Pollen, 0, 100);
    }
}
