using BugStrategy.CommandsCore;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class MissionEditorCommandsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
            BindRepository();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<MissionEditorCommandsFactory>().FromNew().AsSingle();
        }

        private void BindRepository()
        {
            Container.BindInterfacesAndSelfTo<CommandsRepository>().FromNew().AsSingle();
        }
    }
}