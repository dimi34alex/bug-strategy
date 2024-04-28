using System;
using System.Collections.Generic;


[Serializable]
public class ResourceGlobalStorageInspectorVisualisation
{
    private AffiliationEnum _affiliation;

    private List<ResourceGlobalStorageInspectorVisualisationCell> _cells =
        new List<ResourceGlobalStorageInspectorVisualisationCell>();

    public ResourceGlobalStorageInspectorVisualisation(AffiliationEnum affiliation, TeamResourceRepository teamResourceRepository)
    {
        _affiliation = affiliation;

        foreach (var s in teamResourceRepository.ResourceRepository.Resources)
            _cells.Add(new ResourceGlobalStorageInspectorVisualisationCell(s.Key, s.Value));
    }
}

[Serializable]
public class ResourceGlobalStorageInspectorVisualisationCell
{
    private ResourceID _resourceID;
    private float _resourceCapacity;
    private float _resourceValue;

    private readonly ResourceBase _resourceBase;

    public ResourceGlobalStorageInspectorVisualisationCell(ResourceID resourceID, ResourceBase resourceBase)
    {
        _resourceID = resourceID;
        _resourceBase = resourceBase;

        resourceBase.OnChange += Update;
        Update();
    }

    private void Update()
    {
        _resourceCapacity = _resourceBase.Capacity;
        _resourceValue = _resourceBase.CurrentValue;
    }
}
