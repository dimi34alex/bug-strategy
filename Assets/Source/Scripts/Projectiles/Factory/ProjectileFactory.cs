using BugStrategy.Factory;
using Zenject;

namespace BugStrategy.Projectiles.Factory
{
    public class ProjectileFactory : ObjectsFactoryBase<ProjectileType, ProjectileBase>
    {
        public ProjectileFactory(DiContainer diContainer, ProjectilesPrefabsConfig config) 
            : base(diContainer, config, "Projectiles") { }
    }
}