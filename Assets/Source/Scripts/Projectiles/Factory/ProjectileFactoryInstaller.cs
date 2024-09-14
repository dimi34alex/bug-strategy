using Zenject;

namespace BugStrategy.Projectiles.Factory
{
    public class ProjectileFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var factory = FindObjectOfType<ProjectileFactory>(true);
            Container.Bind<ProjectileFactory>().FromInstance(factory).AsSingle();
        }
    }
}