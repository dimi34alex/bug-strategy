using Zenject;

namespace Source.Scripts.ResourcesSystem.ResourcesGlobalStorage
{
    public class TeamsResourceGlobalStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TeamsResourcesGlobalStorage>().FromNew().AsSingle();
        }
    }
}
