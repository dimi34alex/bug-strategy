using Zenject;

namespace BugStrategy.MiniMap
{
    public class MissionEditorMiniMapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MissionEditorMiniMapObjViewFactory>().FromNew().AsSingle();
        }
    }
}