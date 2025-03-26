using System;
using System.Collections.Generic;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class TilesRepository : GridRepository<Tile>, IDisposable
    {
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