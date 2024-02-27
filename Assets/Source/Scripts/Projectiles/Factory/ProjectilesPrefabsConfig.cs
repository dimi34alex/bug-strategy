using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Factory
{
    [CreateAssetMenu(fileName = nameof(ProjectilesPrefabsConfig), menuName = "Configs/" + nameof(ProjectilesPrefabsConfig))]
    public class ProjectilesPrefabsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ProjectileType, ProjectileBase> data;

        public IReadOnlyDictionary<ProjectileType, ProjectileBase> Data => data;
    }
}