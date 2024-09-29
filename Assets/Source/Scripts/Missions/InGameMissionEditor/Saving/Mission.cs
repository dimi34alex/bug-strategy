using System;
using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Saving
{
    [Serializable]
    public class Mission
    {
        [SerializeField] private List<TilePair> groundTiles;
        [SerializeField] private List<ConstructionPair> constructions;
        [SerializeField] private List<ResourceSourcePair> resourceSources;

        public IReadOnlyList<TilePair> GroundTiles => groundTiles;
        public IReadOnlyList<ConstructionPair> Constructions => constructions;
        public IReadOnlyList<ResourceSourcePair> ResourceSources => resourceSources;
        
        public void SetGroundTiles(IReadOnlyDictionary<GridKey3, Tile> newTiles)
        {
            groundTiles = new List<TilePair>(newTiles.Count);
            foreach (var tile in newTiles)
            {
                var d = tile.Value.GetComponent<MissionEditorTileId>();
                groundTiles.Add(new TilePair(d.ID, tile.Key));
            }
        }
        
        public void SetResourceSources(IReadOnlyDictionary<GridKey3, ResourceSourceBase> newTiles)
        {
            resourceSources = new List<ResourceSourcePair>(newTiles.Count);
            foreach (var tile in newTiles)
            {
                var d = tile.Value.GetComponent<MissionEditorTileId>();
                resourceSources.Add(new ResourceSourcePair(d.ID, tile.Key));
            }
        }

        [Serializable]
        public struct TilePair
        {
            public int Id;
            public GridKey3 Position;

            public TilePair(int id, GridKey3 position)
            {
                Id = id;
                Position = position;
            }
        }
        
        [Serializable]
        public struct ConstructionPair
        {
            public ConstructionID Id;
            public AffiliationEnum Affiliation;
            public GridKey3 Position;
        }

        [Serializable]
        public struct ResourceSourcePair
        {
            public int Id;
            public GridKey3 Position;

            public ResourceSourcePair(int id, GridKey3 position)
            {
                Id = id;
                Position = position;
            } 
        }
    }
}