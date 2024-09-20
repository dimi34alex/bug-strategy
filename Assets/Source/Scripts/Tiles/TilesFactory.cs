using System;
using BugStrategy.ResourceSources;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy.Tiles
{
    public class TilesFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TilesConfig _tilesConfig;
        private readonly Transform _parent;
        
        public event Action<Tile> OnCreate; 
        
        public TilesFactory(DiContainer diContainer, TilesConfig tilesConfig)
        {
            _diContainer = diContainer;
            _tilesConfig = tilesConfig;

            _parent = new GameObject { transform = { name = "Tiles" } }.transform;
        }

        public Tile Create(Vector3 position, Quaternion rotation)
        {
            var tileIndex = Random.Range(0, _tilesConfig.Tiles.Count);
            return Create(tileIndex, position, rotation);
        }
        
        public Tile Create(int tileIndex, Vector3 position, Quaternion rotation) 
            => Create(_tilesConfig.Tiles[tileIndex], position, rotation);

        public Tile Create(Tile prefab, Vector3 position, Quaternion rotation)
        {
            var tile = _diContainer.InstantiatePrefab(prefab, position, rotation, _parent).GetComponent<Tile>();
            OnCreate?.Invoke(tile);
            return tile;
        }
        
        public ResourceSourceBase CreateRs(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var tile = _diContainer.InstantiatePrefab(prefab, position, rotation, null);
            return tile.GetComponent<ResourceSourceBase>();
        }
    }
}