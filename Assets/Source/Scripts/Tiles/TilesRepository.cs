using System;
using System.Collections.Generic;

namespace BugStrategy.Tiles
{
    public class TilesRepository : IDisposable
    {
        private readonly List<Tile> _tiles = new(32);
        private readonly TilesFactory _tilesFactory;
        
        public IEnumerable<Tile> Tiles => _tiles;

        public TilesRepository(TilesFactory tilesFactory)
        {
            _tilesFactory = tilesFactory;
            _tilesFactory.OnCreate += Add;
        }

        public void ManualAdd(Tile tile) 
            => Add(tile);

        public void ManualRemove(Tile tile) 
            => Remove(tile);
        
        private void Add(Tile tile)
        {
            if (_tiles.Contains(tile))
                return;

            tile.OnDestroyed += Remove;
            _tiles.Add(tile);
        }

        private void Remove(Tile tile)
        {
            tile.OnDestroyed -= Remove;
            _tiles.Remove(tile);
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