using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TilesConfig _config;
        private readonly TilesRepository _tilesRepository;
        private readonly Transform _parent;
        private readonly List<int> _keys;
        
        public event Action<Tile> OnCreate;

        protected TilesFactory(DiContainer diContainer, TilesConfig config, TilesRepository tilesRepository)
        {
            _diContainer = diContainer;
            _config = config;
            _tilesRepository = tilesRepository;
            _keys = _config.GetData().Keys.ToList();

            _parent = new GameObject { transform = { name = "Tiles" } }.transform;
        }

        public IReadOnlyList<int> GetKeys() 
            => _keys;

        public Tile Create(int id, Vector3 position, bool autoRegistration = true)
            => Create(id, position, Quaternion.identity, autoRegistration);
        
        public Tile Create(int id, Vector3 position, Quaternion rotation, bool autoRegistration = true)
        { 
            var tile = GetObjectInstance(id);
            tile.transform.SetPositionAndRotation(position, rotation);

            if (autoRegistration)
                _tilesRepository.Add(tile);
            
            OnCreate?.Invoke(tile);
            return tile;
        }

        protected virtual Tile GetObjectInstance(int id) 
            => InstantiateNewObject(id);

        protected Tile InstantiateNewObject(int id) 
            => _diContainer.InstantiatePrefab(_config.GetData()[id], _parent).GetComponent<Tile>();
    }
}