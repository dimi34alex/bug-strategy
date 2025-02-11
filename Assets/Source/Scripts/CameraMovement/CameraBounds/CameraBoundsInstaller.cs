using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraBoundsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CameraBounds>().FromNew().AsSingle();
        }
    }
}