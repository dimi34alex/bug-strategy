using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsRepository : IConstructionGrid
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<Vector3Int, ConstructionCellData> _constructions;

    public IReadOnlyCollection<Vector3Int> Positions => _constructions.Keys;

    public ConstructionsRepository()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
            throw new NullReferenceException();

        _constructions = new Dictionary<Vector3Int, ConstructionCellData>();
    }

    public bool ConstructionExist(Vector3Int position)
    {
        return _constructions.ContainsKey(position);
    }
    
    public bool ConstructionExist<TType>(Vector3Int position) where TType : IConstruction
    {
        return ConstructionExist(position) && GetConstruction(position) is TType;
    }

    public void AddConstruction(Vector3Int position, ConstructionBase construction)
    {
        if (_constructions.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        _constructions.Add(position, new ConstructionCellData(construction));
    }

    public ConstructionBase GetConstruction(Vector3Int position)
    {
        if (!_constructions.ContainsKey(position))
            throw new Exception($"Position {position} not found");

        return _constructions[position].Construction;
    }

    public Vector3 RoundPositionToGrid(Vector3 position)
    {
        return position.Round(_constructionConfig.GridTileSize);
    }
}
