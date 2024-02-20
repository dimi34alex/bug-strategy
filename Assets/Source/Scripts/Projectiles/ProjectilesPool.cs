using System;
using System.Collections.Generic;
using Factories;
using UnityEngine;
using Zenject;

namespace Projectiles
{
    public class ProjectilesPool : MonoBehaviour
    {
        [Inject] private ProjectileFactory _factory;

        private Pool<ProjectileBase, ProjectileType> _pool;

        private readonly List<ProjectileBase> _busyElements = new List<ProjectileBase>();
        
        private void Awake()
        {
            _pool = new Pool<ProjectileBase, ProjectileType>(ProjectileInstantiate);
        }

        private void Update()
        {
            var time = Time.deltaTime;
            foreach (var projectile in _busyElements)
                projectile.HandleUpdate(time);
        }

        public TProjectile Extract<TProjectile>(ProjectileType projectileType) where TProjectile : ProjectileBase
        {
            var projectile = _pool.ExtractElement(projectileType);
            if (!projectile.TryGetComponent<TProjectile>(out var tProjectile))
                throw new NullReferenceException($"Object with Key {projectileType} " +
                                                 $"dont have script {typeof(TProjectile).Name}");

            _busyElements.Add(projectile);
            
            return tProjectile;
        }
        
        private ProjectileBase ProjectileInstantiate(ProjectileType projectileType)
        {
            var projectile = _factory.Create(projectileType);
            projectile.ElementReturnEvent += RemoveBusyElement;
            return projectile;
        }

        private void RemoveBusyElement(ProjectileBase projectile) => _busyElements.Remove(projectile);
    }
}