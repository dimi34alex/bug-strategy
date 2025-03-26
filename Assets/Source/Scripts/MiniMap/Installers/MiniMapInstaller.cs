using Zenject;

namespace BugStrategy.MiniMap
{
    public class MiniMapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MiniMapObjViewFactory>().FromNew().AsSingle();
        }
    }
}