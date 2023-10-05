using Zenject;

public class MiniMapIconFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        IMiniMapIconFactory miniMapIconFactory = FindObjectOfType<MiniMapIconFactory>(true);
        Container.BindInstance(miniMapIconFactory).AsSingle();
    }
}
