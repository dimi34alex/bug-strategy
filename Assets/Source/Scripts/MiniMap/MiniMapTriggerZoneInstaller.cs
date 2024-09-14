using Zenject;

namespace BugStrategy.MiniMap
{
    public class MiniMapTriggerZoneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            IMiniMapTriggerZone miniMapTriggerZone = FindObjectOfType<MiniMapTriggerZone>(true);
            Container.BindInstance(miniMapTriggerZone).AsSingle();
        }
    }
}