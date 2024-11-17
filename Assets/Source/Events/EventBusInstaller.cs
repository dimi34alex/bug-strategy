using Avastrad.EventBusFramework;
using Zenject;

namespace BugStrategy.Events
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EventBus>().FromNew().AsSingle();
        }
    }
}