using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public abstract class GridRepository<TValue> : IGridRepository
    {
        private readonly GridConfig _gridConfig;
        private readonly Dictionary<GridKey3, TValue> _tiles;
        private readonly HashSet<GridKey3> _blockedCells;

        private GridBlockChecker _gridBlockChecker;

        public IReadOnlyDictionary<GridKey3, TValue> Tiles => _tiles;
        public IReadOnlyCollection<GridKey3> Positions => _tiles.Keys;

        public GridRepository(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;

            _tiles = new Dictionary<GridKey3, TValue>();
            _blockedCells = new HashSet<GridKey3>();
        }

        public void SetGridBlocker(GridBlockChecker gridBlocker) 
            => _gridBlockChecker = gridBlocker; 

        public bool Exist(Vector3 position, bool blockIgnore = true, bool includeGridBlocker = true) 
            => _tiles.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position) ||
               includeGridBlocker && _gridBlockChecker != null && _gridBlockChecker.Blocked(position);

        public bool Exist<TType>(Vector3 position, bool blockIgnore = true) where TType : IConstruction 
            => Exist(position, blockIgnore) && Get(position) is TType;

        public bool TryAdd(Vector3 position, TValue tile)
        {
            if (Exist(position, false))
                return false;

            _tiles.Add(position, tile);
            return true;
        }
        
        public void Add(Vector3 position, TValue tile)
        {
            if (!TryAdd(position, tile))
                throw new Exception($"Tile can be placed in position: {position}");
        }

        public TValue Get(Vector3 position, bool withExtract = false)
        {
            if (!_tiles.ContainsKey(position))
                throw new Exception($"Position {position} not found");

            var construction = _tiles[position];

            if (withExtract)
                _tiles.Remove(position);

            return construction;
        }

        public Vector3 RoundPositionToGrid(Vector3 position)
            => _gridConfig.RoundPositionToGrid(position);

        public void BlockCell(Vector3 position) 
            => _blockedCells.Add(position);

        public void UnblockCell(Vector3 position) 
            => _blockedCells.Remove(position);

        public bool CellIsBlocked(Vector3 position) 
            => _blockedCells.Contains(position);
    }
}