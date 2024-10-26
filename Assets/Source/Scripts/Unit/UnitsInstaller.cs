using BugStrategy.Unit.Factory;
using Zenject;

namespace BugStrategy.Unit
{
    public class UnitsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<UnitRepository>().FromNew().AsSingle();
        
        private void BindFactory()
        {
            var unitFactory = FindObjectOfType<UnitFactory>(true);
            Container.Bind<UnitFactory>().FromInstance(unitFactory).AsSingle();
        }
    }
}