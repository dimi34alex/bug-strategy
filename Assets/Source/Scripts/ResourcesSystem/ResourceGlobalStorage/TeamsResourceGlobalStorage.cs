using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamsResourceGlobalStorage : MonoBehaviour, IResourceGlobalStorage
{
    [SerializeField] private ResourceGlobalStorageConfig config;
    [SerializeField] private ResourceConfig[] resourceConfigs;

    private List<ResourceGlobalStorageInspectorVisualisation> _visualisation = new List<ResourceGlobalStorageInspectorVisualisation>();
    
    private readonly Dictionary<AffiliationEnum, TeamResourceRepository> _resourceRepositories =
        new Dictionary<AffiliationEnum, TeamResourceRepository>();
        
    private void Awake()
    {
        foreach (var initialState in config.InitialStates)
        {
            var newTeamResourceRepository = new TeamResourceRepository(resourceConfigs, initialState.Value);
            _resourceRepositories.Add(initialState.Key, newTeamResourceRepository);
            _visualisation.Add(new ResourceGlobalStorageInspectorVisualisation(initialState.Key, newTeamResourceRepository));
        }
    }
        
    public ResourceRepository GetAffiliationResourceRepository(AffiliationEnum affiliation)
    {
        if (!_resourceRepositories.ContainsKey(affiliation))
            throw new ArgumentException();

        return _resourceRepositories[affiliation].ResourceRepository;
    }
        
    public void ChangeCapacity(AffiliationEnum affiliation, ResourceID resourceID, float capacity)
    {
        ResourceBase resourceBase = _resourceRepositories[affiliation].GetResource(resourceID);
        resourceBase.SetCapacity(resourceBase.Capacity + capacity);
    }

    public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value)
    {
        ResourceBase resourceBase = _resourceRepositories[affiliation].GetResource(resourceID);
        resourceBase.ChangeValue(value);
    }
    
    public ResourceBase GetResource(AffiliationEnum affiliation, ResourceID resourceID)
    {
        if (!_resourceRepositories.ContainsKey(affiliation))
            throw new ArgumentException($"Key was not present in dictionary: {affiliation}");
            
        return _resourceRepositories[affiliation].GetResource(resourceID);
    }
}