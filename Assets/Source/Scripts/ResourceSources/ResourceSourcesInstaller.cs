using Zenject;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourcesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<ResourceSourcesRepository>().FromNew().AsSingle();

        private void BindFactory() 
            => Container.BindInterfacesAndSelfTo<ResourceSourceFactory>().FromNew().AsSingle();
    }
}