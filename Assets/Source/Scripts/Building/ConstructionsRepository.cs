using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsRepository : IConstructionGrid
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<GridKey3, ConstructionCellData> _constructions;
    private readonly HashSet<GridKey3> _blockedCells;

    public IReadOnlyCollection<GridKey3> Positions => _constructions.Keys;

    public ConstructionsRepository()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
            throw new NullReferenceException();

        _constructions = new Dictionary<GridKey3, ConstructionCellData>();
        _blockedCells = new HashSet<GridKey3>();
    }

    public bool ConstructionExist(Vector3 position, bool blockIgnore = true)
    {
        return _constructions.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position);
    }
    
    public bool ConstructionExist<TType>(Vector3 position, bool blockIgnore = true) where TType : IConstruction
    {
        return ConstructionExist(position, blockIgnore) && GetConstruction(position) is TType;
    }

    public void AddConstruction(Vector3 position, ConstructionBase construction)
    {
        if (_constructions.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        _constructions.Add(position, new ConstructionCellData(construction));
    }

    public ConstructionBase GetConstruction(Vector3 position, bool withExtract = false)
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
        float xPosition = position.x.Round(_constructionConfig.HexagonsOffcets.x / 2);

        int horizontalLineIndex =
            Convert.ToInt32(xPosition / (_constructionConfig.HexagonsOffcets.x / 2f));

        float zPosition = position.z.Round(_constructionConfig.HexagonsOffcets.y);

        int verticalLineIndex = Convert.ToInt32(zPosition / _constructionConfig.HexagonsOffcets.y);

        if (horizontalLineIndex % 2 == 0 && verticalLineIndex % 2 != 0 || horizontalLineIndex % 2 != 0 && verticalLineIndex % 2 == 0)
            zPosition += _constructionConfig.HexagonsOffcets.y;

        return new Vector3(xPosition, 0f, zPosition);
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
