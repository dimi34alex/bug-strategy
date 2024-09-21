using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Factory;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
    [CreateAssetMenu(fileName = nameof(ResourceSourcesConfig), menuName = "Configs/" + nameof(ResourceSourcesConfig))]
    public class ResourceSourcesConfig : ScriptableObject,  IFactoryConfig<int, ResourceSourceBase>, ISingleConfig
    {
        [SerializeField] private List<ResourceSourceBase> resourceSources;

        public IReadOnlyList<ResourceSourceBase> ResourceSources => resourceSources;
        
        private Dictionary<int, ResourceSourceBase> _resourceSource = null;

        /// <summary>
        /// Result cached
        /// </summary>
        public IReadOnlyDictionary<int, ResourceSourceBase> GetData()
        {
            if (_resourceSource != null)
                return _resourceSource;
            
            _resourceSource = new Dictionary<int, ResourceSourceBase>(resourceSources.Count);
            for (int i = 0; i < resourceSources.Count; i++) 
                _resourceSource.Add(i, resourceSources[i]);

            return _resourceSource;
        }
    }
}