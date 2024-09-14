using System;
using System.Collections.Generic;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionsRepository : IConstructionGrid
    {
        private readonly GridConfig _constructionConfig;
        private readonly Dictionary<GridKey3, ConstructionCellData> _constructions;
        private readonly HashSet<GridKey3> _blockedCells;

        public IReadOnlyCollection<GridKey3> Positions => _constructions.Keys;

        public ConstructionsRepository()
        {
            _constructionConfig = ConfigsRepository.ConfigsRepository.FindConfig<GridConfig>() ??
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
            => _constructionConfig.HexagonsOffsets;

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
}
