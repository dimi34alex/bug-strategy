using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourceFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ResourceSourcesConfig _resourceSourcesConfig;
        private readonly Transform _parent;
        
        public event Action<ResourceSourceBase> OnCreate; 
        
        public ResourceSourceFactory(DiContainer diContainer, ResourceSourcesConfig resourceSourcesConfig)
        {
            _diContainer = diContainer;
            _resourceSourcesConfig = resourceSourcesConfig;

            _parent = new GameObject { transform = { name = "ResourceSources" } }.transform;
        }

        public ResourceSourceBase Create(Vector3 position, Quaternion rotation)
        {
            var index = Random.Range(0, _resourceSourcesConfig.ResourceSources.Count);
            return Create(index, position, rotation);
        }
        
        public ResourceSourceBase Create(int tileIndex, Vector3 position, Quaternion rotation) 
            => Create(_resourceSourcesConfig.ResourceSources[tileIndex], position, rotation);

        public ResourceSourceBase Create(ResourceSourceBase prefab, Vector3 position, Quaternion rotation)
        {
            var resourceSource = _diContainer.InstantiatePrefab(prefab, position, rotation, _parent).GetComponent<ResourceSourceBase>();
            OnCreate?.Invoke(resourceSource);
            return resourceSource;
        }
    }
}