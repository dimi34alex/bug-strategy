using Zenject;

public class ResourceGlobalStorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TeamsResourceGlobalStorage>().FromNew().AsSingle();
    }
}
