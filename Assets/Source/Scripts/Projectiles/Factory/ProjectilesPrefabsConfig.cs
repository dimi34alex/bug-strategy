using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Projectiles.Factory
{
    [CreateAssetMenu(fileName = nameof(ProjectilesPrefabsConfig), menuName = "Configs/" + nameof(ProjectilesPrefabsConfig))]
    public class ProjectilesPrefabsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ProjectileType, ProjectileBase> data;

        public IReadOnlyDictionary<ProjectileType, ProjectileBase> Data => data;
    }
}
