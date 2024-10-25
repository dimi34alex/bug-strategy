using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy.Factories
{
    public class FactoryWithId<TId, TResult>
        where TResult : MonoBehaviour
    {
        private readonly DiContainer _diContainer;
        private readonly IFactoryConfig<TId, TResult> _config;
        private readonly Transform _parent;
        private readonly List<TId> _keys;
        
        public event Action<TResult> OnCreate;

        protected FactoryWithId(DiContainer diContainer, IFactoryConfig<TId, TResult> config, string parentName)
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
            var result = GetObjectInstance(id);
            result.transform.SetPositionAndRotation(position, rotation);
            OnCreate?.Invoke(result);
            return result;
        }

        protected virtual TResult GetObjectInstance(TId id) 
            => InstantiateNewObject(id);

        protected TResult InstantiateNewObject(TId id) 
            => _diContainer.InstantiatePrefab(_config.GetData()[id], _parent).GetComponent<TResult>();
    }
}