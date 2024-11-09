using System;
using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Projectiles.Factory
{
    public class ProjectilesFactory : FactoryWithIdPool<ProjectileType, ProjectileBase>, IDisposable
    {
        private readonly ProjectilesRepository _projectilesRepository;
        
        public ProjectilesFactory(DiContainer diContainer, ProjectilesPrefabsConfig config, ProjectilesRepository projectilesRepository) 
            : base(diContainer, config, "Projectiles")
        {
            _projectilesRepository = projectilesRepository;
            OnCreate += AddProjectileToRepository;
        }

        private void AddProjectileToRepository(ProjectileBase projectile) 
            => _projectilesRepository.AddProjectile(projectile);

        public void Dispose() 
            => OnCreate -= AddProjectileToRepository;
    }
}