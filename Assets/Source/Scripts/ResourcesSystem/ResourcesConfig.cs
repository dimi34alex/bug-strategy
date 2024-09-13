using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.ResourcesSystem
{
    [CreateAssetMenu(fileName = nameof(ResourcesConfig), menuName = "Configs/Resources/" + nameof(ResourcesConfig))]
    public class ResourcesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<ResourceConfig> resourceConfigs;

        public IReadOnlyList<ResourceConfig> ResourceConfigs => resourceConfigs;
    }
}