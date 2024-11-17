using Zenject;

namespace BugStrategy.Unit.Ants.GetToolOrdere
{
    public class PlayerAntsGetToolOrdererInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerAntsGetToolOrderer>().FromNew().AsSingle();
        }
    }
}