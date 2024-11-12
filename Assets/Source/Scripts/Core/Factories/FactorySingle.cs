using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy.Factories
{
    public class FactorySingle<TResult>
        where TResult : MonoBehaviour
    {
        private readonly DiContainer _diContainer;
        private readonly TResult _prefab;
        private readonly Transform _parent;
        
        public event Action<TResult> OnCreate;

        protected FactorySingle(DiContainer diContainer, TResult prefab, string parentName)
        {
            _diContainer = diContainer;
            _prefab = prefab;

            _parent = new GameObject { transform = { name = parentName } }.transform;
        }

        public TResult Create()
            => Create(Vector3.zero, Quaternion.identity);

        public TResult Create(Vector3 position)
            => Create(position, Quaternion.identity);

        public TResult Create(Vector3 position, Quaternion rotation)
        { 
            var result = GetObjectInstance();
            result.transform.SetPositionAndRotation(position, rotation);
            OnCreate?.Invoke(result);
            return result;
        }

        protected virtual TResult GetObjectInstance() 
            => InstantiateNewObject();

        protected TResult InstantiateNewObject() 
            => _diContainer.InstantiatePrefab(_prefab, _parent).GetComponent<TResult>();
    }
}