using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public abstract class GridRepository<TValue> : IGridRepository
    {
        private readonly Dictionary<GridKey3, TValue> _tiles = new();
        private readonly HashSet<GridKey3> _blockedCells = new();

        private GridBlockChecker _gridBlockChecker;

        public IReadOnlyDictionary<GridKey3, TValue> Tiles => _tiles;
        public IReadOnlyCollection<GridKey3> Positions => _tiles.Keys;

        public void SetGridBlocker(GridBlockChecker gridBlocker) 
            => _gridBlockChecker = gridBlocker; 

        public bool FreeInOtherGrids(Vector3 position) 
            => _gridBlockChecker == null || !_gridBlockChecker.Blocked(position);
        
        public bool Exist(Vector3 position, bool blockIgnore = true, bool includeGridBlocker = true) 
            => _tiles.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position) ||
               includeGridBlocker && !FreeInOtherGrids(position);

        public bool Exist<TType>(Vector3 position, bool blockIgnore = true, bool includeGridBlocker = true) 
            where TType : IConstruction 
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
                throw new Exception($"Tile cant be placed in position: {position}");
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

        public void BlockCell(Vector3 position) 
            => _blockedCells.Add(position);

        public void UnblockCell(Vector3 position) 
            => _blockedCells.Remove(position);

        public bool CellIsBlocked(Vector3 position) 
            => _blockedCells.Contains(position);
    }
}