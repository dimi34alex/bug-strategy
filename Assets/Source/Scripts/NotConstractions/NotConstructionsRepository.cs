using System;
using System.Collections.Generic;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionsRepository : INotConstructionGrid
    {
        private readonly GridConfig _notConstructionConfig;
        private readonly Dictionary<GridKey3, NotConstructionCellData> _notConstructions;
        private readonly HashSet<GridKey3> _blockedCells;

        public IReadOnlyCollection<GridKey3> Positions => _notConstructions.Keys;

        public NotConstructionsRepository()
        {
            _notConstructionConfig = ConfigsRepository.ConfigsRepository.FindConfig<GridConfig>() ??
                                  throw new NullReferenceException();

            _notConstructions = new Dictionary<GridKey3, NotConstructionCellData>();
            _blockedCells = new HashSet<GridKey3>();
        }

        public bool NotConstructionExist(Vector3 position, bool blockIgnore = true)
        {
            return _notConstructions.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position);
        }
    
        public bool NotConstructionExist<TType>(Vector3 position, bool blockIgnore = true) where TType : INotConstruction
        {
            return NotConstructionExist(position, blockIgnore) && GetNotConstruction(position) is TType;
        }

        public void AddNotConstruction(Vector3 position, NotConstructionBase notConstruction)
        {
            if (_notConstructions.ContainsKey(position))
                throw new Exception($"Position {position} already exist in grid");

            _notConstructions.Add(position, new NotConstructionCellData(notConstruction));
        }

        public NotConstructionBase GetNotConstruction(Vector3 position, bool withExtract = false)
        {
            if (!_notConstructions.ContainsKey(position))
                throw new Exception($"Position {position} not found");

            NotConstructionBase notConstruction = _notConstructions[position].NotConstruction;

            if (withExtract)
                _notConstructions.Remove(position);

            return notConstruction;
        }

        public Vector3 RoundPositionToGrid(Vector3 position)
            => _notConstructionConfig.RoundPositionToGrid(position);

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
