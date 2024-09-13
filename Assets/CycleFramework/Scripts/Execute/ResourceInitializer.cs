using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Missions;
using UnityEngine;
using Zenject;

public class ResourceInitializer : CycleInitializerBase
{
    [Inject] private MissionData _missionData;
    
    [SerializeField] private ResourceConfig[] _resourceConfigs;
    private void Awake()
    {   
        ResourceRepository resourceRepository = _missionData.ResourceRepository;
        resourceRepository = new ResourceRepository(_resourceConfigs);
        resourceRepository.CreateResource(ResourceID.Bees_Wax,0,100);
        resourceRepository.CreateResource(ResourceID.Pollen, 0, 100);
    }
}
