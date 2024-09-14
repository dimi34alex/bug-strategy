using Zenject;

namespace BugStrategy.ResourcesSystem.ResourcesGlobalStorage
{
    public class TeamsResourceGlobalStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TeamsResourcesGlobalStorage>().FromNew().AsSingle();
        }
    }
}
