using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class TilesRepository : IDisposable
    {
        private readonly Dictionary<GridKey3, Tile> _tilesByPositions = new(32);
        private readonly List<Tile> _tiles = new(32);
        private readonly TilesFactory _tilesFactory;
        
        public IEnumerable<Tile> Tiles => _tiles;

        public TilesRepository(TilesFactory tilesFactory)
        {
            _tilesFactory = tilesFactory;
            _tilesFactory.OnCreate += Add;
        }

        public bool Exist(Vector3 position) 
            => _tilesByPositions.ContainsKey(position);

        public Tile Get(Vector3 position)
        {
            if (!_tilesByPositions.ContainsKey(position))
                throw new Exception($"Position {position} not found");

            var tile = _tilesByPositions[position];

            return tile;
        }
        
        private void Add(Tile tile)
        {
            var position = tile.transform.position;
            if (_tilesByPositions.ContainsKey(position))
                throw new Exception($"Position {position} already exist in grid");

            tile.OnDestroyed += Remove;
            _tilesByPositions.Add(position, tile);
            _tiles.Add(tile);
        }

        private void Remove(Tile tile)
        {
            tile.OnDestroyed -= Remove;
            _tiles.Remove(tile);
            _tilesByPositions.Remove(tile.transform.position);
        }

        public void Dispose()
        {
            _tilesFactory.OnCreate -= Add;
            foreach (var tile in _tiles)
            {
                if (tile != null) 
                    tile.OnDestroyed -= Remove;
            }
        }
    }
}