using System;
using System.Collections.Generic;
using EnumValuesExtension;
using Source.Scripts.Missions;
using UnityEngine;
using Zenject;

namespace Projectiles.Factory
{
    public class ProjectileFactory : MonoBehaviour
    {
        [Inject] private MissionData _missionData;
        [Inject] private ProjectilesPrefabsConfig _projectilesPrefabsConfig;
        [Inject] private DiContainer _container;

        private Pool<ProjectileBase, ProjectileType> _pool;
        private readonly Dictionary<ProjectileType, Transform> _parents = new Dictionary<ProjectileType, Transform>();
        
        private void Awake()
        {
            _pool = new Pool<ProjectileBase, ProjectileType>(InstantiateProjectile);
            
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
            var projectile = _pool.ExtractElement(projectileType);
            _missionData.ProjectilesRepository.AddProjectile(projectile);
            
            return projectile;
        }

        private ProjectileBase InstantiateProjectile(ProjectileType projectileType)
        {
            if (!_projectilesPrefabsConfig.Data.ContainsKey(projectileType))
                throw new IndexOutOfRangeException($"Type {projectileType} dont present in {_projectilesPrefabsConfig}");
                    
            var projectile = 
                _container.InstantiatePrefab(_projectilesPrefabsConfig.Data[projectileType], _parents[projectileType]);
            
            if (!projectile.TryGetComponent<ProjectileBase>(out var tProjectile))
                throw new NullReferenceException($"Prefab with Key {projectileType}" +
                                                 $" dont have script {nameof(ProjectileBase)}");
            
            return tProjectile;
        }
    }
}