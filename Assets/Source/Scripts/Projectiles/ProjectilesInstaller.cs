using BugStrategy.Projectiles.Factory;
using Zenject;

namespace BugStrategy.Projectiles
{
    public class ProjectilesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<ProjectilesRepository>().FromNew().AsSingle();

        private void BindFactory() 
            => Container.BindInterfacesAndSelfTo<ProjectileFactory>().FromNew().AsSingle();
    }
}