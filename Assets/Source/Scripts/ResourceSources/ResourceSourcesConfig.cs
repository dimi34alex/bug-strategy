using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
    [CreateAssetMenu(fileName = nameof(ResourceSourcesConfig), menuName = "Configs/" + nameof(ResourceSourcesConfig))]
    public class ResourceSourcesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<ResourceSourceBase> resourceSources;

        public IReadOnlyList<ResourceSourceBase> ResourceSources => resourceSources;
    }
}