using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.ResourcesSystem
{
    [CreateAssetMenu(fileName = nameof(ResourcesConfig), menuName = "Configs/Resources/" + nameof(ResourcesConfig))]
    public class ResourcesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<ResourceConfig> resourceConfigs;

        public IReadOnlyList<ResourceConfig> ResourceConfigs => resourceConfigs;
    }
}