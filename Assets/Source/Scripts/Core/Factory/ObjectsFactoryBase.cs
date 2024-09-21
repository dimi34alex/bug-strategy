using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace BugStrategy.Factory
{
    public abstract class ObjectsFactoryBase<TId, TResult, TConfig>
        where TConfig : IFactoryConfig<TId, TResult>
        where TResult : Object
    {
        private readonly DiContainer _diContainer;
        private readonly TConfig _config;
        private readonly Transform _parent;
        private readonly List<TId> _keys;
        
        public event Action<TResult> OnCreate;

        protected ObjectsFactoryBase(DiContainer diContainer, TConfig config, string parentName)
        {
            _diContainer = diContainer;
            _config = config;
            _keys = _config.GetData().Keys.ToList(); 

            _parent = new GameObject { transform = { name = parentName } }.transform;
        }

        public IReadOnlyList<TId> GetKeys() 
            => _keys;

        public TResult Create(TId id)
            => Create(id, Vector3.zero, Quaternion.identity);

        public TResult Create(TId id, Vector3 position)
            => Create(id, position, Quaternion.identity);
        
        public TResult Create(Vector3 position)
        {
            var idIndex = Random.Range(0, _keys.Count);
            var id = _keys[idIndex];
            return Create(id, position, Quaternion.identity);
        }

        public TResult Create(TId id, Vector3 position, Quaternion rotation)
        { 
            var tile = _diContainer.InstantiatePrefab(_config.GetData()[id], position, rotation, _parent).GetComponent<TResult>();
            OnCreate?.Invoke(tile);
            return tile;
        }
    }
}