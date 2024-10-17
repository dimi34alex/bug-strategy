using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Factory;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Projectiles.Factory
{
    [CreateAssetMenu(fileName = nameof(ProjectilesPrefabsConfig), menuName = "Configs/" + nameof(ProjectilesPrefabsConfig))]
    public class ProjectilesPrefabsConfig : ScriptableObject, IFactoryConfig<ProjectileType, ProjectileBase>, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ProjectileType, ProjectileBase> data;

        public IReadOnlyDictionary<ProjectileType, ProjectileBase> GetData() 
            => data;
    }
}
