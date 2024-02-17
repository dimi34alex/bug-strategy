using System;
using System.Collections.Generic;
using EnumValuesExtension;
using Projectiles;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class ProjectileFactory : FactoryBase
    {
        [Inject] private ProjectilesPrefabsConfig _projectilesPrefabsConfig;
        [Inject] private DiContainer _container;

        private readonly Dictionary<ProjectileType, Transform> _parents = new Dictionary<ProjectileType, Transform>();
        
        private void Awake()
        {
            var types = EnumValuesTool.GetValues<ProjectileType>();
            foreach (var type in types)
            {
                var parent = new GameObject()
                {
                    name = type.ToString(),
                    transform = { parent = this.gameObject.transform }
                };
                _parents.Add(type, parent.transform);
            }
        }

        public ProjectileBase Create(ProjectileType projectileType)
        {
            if (!_projectilesPrefabsConfig.Data.ContainsKey(projectileType))
                throw new IndexOutOfRangeException($"Type {projectileType} dont present in {_projectilesPrefabsConfig}");
                    
            var projectile = _container.InstantiatePrefab(_projectilesPrefabsConfig.Data[projectileType], _parents[projectileType]);
            
            if (!projectile.TryGetComponent<ProjectileBase>(out var tProjectile))
                throw new NullReferenceException($"Prefab with Key {projectileType}" +
                                                 $" dont have script {nameof(ProjectileBase)}");
            
            return tProjectile;
        }
    }
}