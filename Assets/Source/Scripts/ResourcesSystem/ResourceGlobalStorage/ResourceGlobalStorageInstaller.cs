using Zenject;

public class ResourceGlobalStorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var rgs = FindObjectOfType<TeamsResourceGlobalStorage>();
        Container.BindInterfacesAndSelfTo<TeamsResourceGlobalStorage>().FromInstance(rgs).AsSingle();
    }
}
