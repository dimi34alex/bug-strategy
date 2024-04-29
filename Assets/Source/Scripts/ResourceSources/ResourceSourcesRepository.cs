using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSourcesRepository
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<GridKey3, ResourceSourceBase> _resourceSources;
    private readonly HashSet<GridKey3> _blockedCells;
        
    public ResourceSourcesRepository()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
                              throw new NullReferenceException();

        _resourceSources = new Dictionary<GridKey3, ResourceSourceBase>();
        _blockedCells = new HashSet<GridKey3>();
    }

    public bool Exist(Vector3 position, bool blockIgnore = true)
    {
        return _resourceSources.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position);
    }

    public void Add(Vector3 position, ResourceSourceBase resourceSource)
    {
        if (_resourceSources.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        _resourceSources.Add(position, resourceSource);
    }
        
    public ResourceSourceBase Get(Vector3 position, bool withExtract = false)
    {
        if (!_resourceSources.ContainsKey(position))
            throw new Exception($"Position {position} not found");

        var resourceSource = _resourceSources[position];

        if (withExtract)
            _resourceSources.Remove(position);

        return resourceSource;
    }
        
    public void BlockCell(Vector3 position)
    {
        _blockedCells.Add(position);
    }

    public void UnblockCell(Vector3 position)
    {
        _blockedCells.Remove(position);
    }

    public bool CellIsBlocked(Vector3 position)
    {
        return _blockedCells.Contains(position);
    }
}