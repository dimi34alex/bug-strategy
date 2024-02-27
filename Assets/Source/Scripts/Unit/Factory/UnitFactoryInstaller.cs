using Zenject;

namespace Unit.Factory
{
    public class UnitFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var unitFactory = FindObjectOfType<UnitFactory>(true);
            Container.Bind<UnitFactory>().FromInstance(unitFactory).AsSingle();
        }
    }
}