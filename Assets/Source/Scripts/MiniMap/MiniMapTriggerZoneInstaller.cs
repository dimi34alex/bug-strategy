using Zenject;
using MiniMapSystem;

public class MiniMapTriggerZoneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        IMiniMapTriggerZone miniMapTriggerZone = FindObjectOfType<MiniMapTriggerZone>(true);
        Container.BindInstance(miniMapTriggerZone).AsSingle();
    }
}