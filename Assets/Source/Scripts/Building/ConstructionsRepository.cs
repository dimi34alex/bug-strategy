using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsRepository : IConstructionGrid
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<Vector3Int, ConstructionCellData> _constructions;
    private readonly HashSet<Vector3Int> _blockedCells;

    public IReadOnlyCollection<Vector3Int> Positions => _constructions.Keys;

    public ConstructionsRepository()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
            throw new NullReferenceException();

        _constructions = new Dictionary<Vector3Int, ConstructionCellData>();
        _blockedCells = new HashSet<Vector3Int>();
    }

    public bool ConstructionExist(Vector3Int position, bool blockIgnore = true)
    {
        return _constructions.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position);
    }
    
    public bool ConstructionExist<TType>(Vector3Int position, bool blockIgnore = true) where TType : IConstruction
    {
        return ConstructionExist(position, blockIgnore) && GetConstruction(position) is TType;
    }

    public void AddConstruction(Vector3Int position, ConstructionBase construction)
    {
        if (_constructions.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        _constructions.Add(position, new ConstructionCellData(construction));
    }

    public ConstructionBase GetConstruction(Vector3Int position, bool withExtract = false)
    {
        if (!_constructions.ContainsKey(position))
            throw new Exception($"Position {position} not found");

        ConstructionBase construction = _constructions[position].Construction;

        if (withExtract)
            _constructions.Remove(position);

        return construction;
    }

    public Vector3 RoundPositionToGrid(Vector3 position)
    {
        return position.Round(_constructionConfig.GridTileSize);
    }

    public void BlockCell(Vector3Int position)
    {
        _blockedCells.Add(position);
    }

    public void UnblockCell(Vector3Int position)
    {
        _blockedCells.Remove(position);
    }

    public bool CellIsBlocked(Vector3Int position)
    {
        return _blockedCells.Contains(position);
    }
}
