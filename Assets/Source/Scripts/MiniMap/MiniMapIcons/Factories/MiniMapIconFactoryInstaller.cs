using Zenject;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class MiniMapIconFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MiniMapIconFactory>().FromNew().AsSingle();
        }
    }
}
