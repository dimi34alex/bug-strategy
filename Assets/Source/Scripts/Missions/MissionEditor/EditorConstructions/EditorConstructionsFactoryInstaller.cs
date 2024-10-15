using Zenject;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstructionsFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EditorConstructionsFactory>().FromNew().AsSingle();
        }
    }
}