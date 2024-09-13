using System;
using System.Collections.Generic;
using Source.Scripts.ResourcesSystem.ResourceGlobalStorage;
using UnityEngine;
using Zenject;

public class TeamsResourceGlobalStorage : IResourceGlobalStorage
{
    private List<ResourceGlobalStorageInspectorVisualisation> _visualisation = new();//dont make it readonly
    private readonly Dictionary<AffiliationEnum, TeamResourceRepository> _resourceRepositories = new();

    public TeamsResourceGlobalStorage(ResourceGlobalStorageConfig config, ResourcesConfig resourceConfigs)
    {
        foreach (var initialState in config.InitialStates)
        {
            var newTeamResourceRepository = new TeamResourceRepository(resourceConfigs.ResourceConfigs, initialState.Value);
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
        var resourceBase = _resourceRepositories[affiliation].GetResource(resourceID);
        resourceBase.SetCapacity(resourceBase.Capacity + capacity);
    }

    public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value)
    {
        var resourceBase = _resourceRepositories[affiliation].GetResource(resourceID);
        resourceBase.ChangeValue(value);
    }
    
    public ResourceBase GetResource(AffiliationEnum affiliation, ResourceID resourceID)
    {
        if (!_resourceRepositories.ContainsKey(affiliation))
            throw new ArgumentException($"Key was not present in dictionary: {affiliation}");
            
        return _resourceRepositories[affiliation].GetResource(resourceID);
    }
}