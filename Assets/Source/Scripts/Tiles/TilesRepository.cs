using System;
using BugStrategy.Grids;

namespace BugStrategy.Tiles
{
    public class TilesRepository : GridRepository<Tile>, IDisposable
    {
        public TilesRepository(GridConfig gridConfig) 
            : base(gridConfig) { }
        
        public void Add(Tile tile)
        {
            tile.OnDestroyed += Remove;
            Add(tile.transform.position, tile);
        }

        public void Remove(Tile tile)
        {
            tile.OnDestroyed -= Remove;
            Get(tile.transform.position, true);
        }

        public void Dispose()
        {
            foreach (var tile in Tiles.Values)
            {
                if (tile != null) 
                    tile.OnDestroyed -= Remove;
            }
        }
    }
}