using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class CommandsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
            BindRepository();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<CommandsFactory>().FromNew().AsSingle();
        }

        private void BindRepository()
        {
            Container.BindInterfacesAndSelfTo<CommandsRepository>().FromNew().AsSingle();
        }
    }
}