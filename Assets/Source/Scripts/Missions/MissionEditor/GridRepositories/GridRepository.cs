using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.GridRepositories
{
    public abstract class GridRepository<TValue> : IGridRepository<TValue>
    {
        private readonly GridConfig _gridConfig;
        private readonly Dictionary<GridKey3, TValue> _tiles = new();
        private readonly HashSet<GridKey3> _blockedCells = new();

        private IGridRepository[] _externalGrids;

        protected GridRepository(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;
        }

        public IReadOnlyDictionary<GridKey3, TValue> Tiles => _tiles;
        public IReadOnlyCollection<GridKey3> Positions => _tiles.Keys;

        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;
        
        public void SetExternalGrids(IGridRepository[] externalGrids) 
            => _externalGrids = externalGrids; 

        public bool FreeInExternalGrids(Vector3 position)
        {
            if (_externalGrids == null || _externalGrids.Length <= 0)
                return true;

            foreach (var gridRepository in _externalGrids)
                if (gridRepository.Exist(position, false, false))
                    return false;

            return true;
        }

        public bool Exist(Vector3 position, bool includeBlockedCells = true, bool includeExternalGrids = true)
        {
            position = RoundPositionToGrid(position);
            return _tiles.ContainsKey(position) || includeBlockedCells && _blockedCells.Contains(position) ||
                   includeExternalGrids && !FreeInExternalGrids(position);
        }

        public virtual bool TryAdd(Vector3 position, TValue tile)
        {
            if (Exist(position))
                return false;

            Add(position, tile);
            return true;
        }
        
        public void Add(Vector3 position, TValue tile)
        {
            position = RoundPositionToGrid(position);
            _tiles.Add(position, tile);
            OnAdd?.Invoke(position);
        }

        public TValue Get(Vector3 position, bool withRemove = false)
        {
            position = RoundPositionToGrid(position);
            if (!_tiles.ContainsKey(position))
                throw new Exception($"Position {position} not found");

            var construction = _tiles[position];

            if (withRemove)
            {
                _tiles.Remove(position);
                OnRemove?.Invoke(position);
            }

            return construction;
        }

        public void BlockCell(Vector3 position)
        {
            position = RoundPositionToGrid(position);
            _blockedCells.Add(position);
        }

        public void UnblockCell(Vector3 position)
        {
            position = RoundPositionToGrid(position);
            _blockedCells.Remove(position);
        }

        public bool CellIsBlocked(Vector3 position)
        {
            position = RoundPositionToGrid(position);
            return _blockedCells.Contains(position);
        }
        
        public Vector3 RoundPositionToGrid(Vector3 position)
            => _gridConfig.RoundPositionToGrid(position);
    }
}