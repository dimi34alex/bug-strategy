using Zenject;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourcesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourceSourceFactory>().FromNew().AsSingle();
        }
    }
}