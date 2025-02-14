using BugStrategy.TechnologiesSystem.Technologies;
using BugStrategy.TechnologiesSystem.Technologies.Configs;
using Zenject;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TechnologiesConfigsProvider>().FromNew().AsSingle();
            Container.Bind<TechnologiesFactory>().FromNew().AsSingle();
            Container.Bind<TechnologiesRepository>().FromNew().AsSingle();
        }
    }
}