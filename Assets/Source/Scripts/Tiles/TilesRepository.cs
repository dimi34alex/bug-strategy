using System;
using System.Collections.Generic;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Tiles
{
    //TODO: replace it by TilesRepository from mission editor
    public class TilesRepository : GroundPositionsRepository, IDisposable
    {
        private readonly TilesFactory _tilesFactory;
        
        public TilesRepository(TilesFactory tilesFactory)
        {
            _tilesFactory = tilesFactory;
            _tilesFactory.OnCreate += Add;
        }

        private void Add(Tile tile)
        {
            tile.OnDestroyed += Remove;
            Add(tile.transform.position, tile);
        }

        private void Remove(Tile tile)
        {
            tile.OnDestroyed -= Remove;
            Get(tile.transform.position, true);
        }

        public void Dispose()
        {
            _tilesFactory.OnCreate -= Add;
            foreach (var tile in Tiles.Values)
            {
                if (tile != null) 
                    tile.OnDestroyed -= Remove;
            }
        }
    }
}