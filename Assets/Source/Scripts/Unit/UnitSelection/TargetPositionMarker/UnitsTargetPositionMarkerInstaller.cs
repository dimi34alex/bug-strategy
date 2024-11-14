using Zenject;

namespace BugStrategy.Unit.UnitSelection.TargetPositionMarker
{
    public class UnitsTargetPositionMarkerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitsTargetPositionMarkerFactory>().FromNew().AsSingle();
        }
    }
}