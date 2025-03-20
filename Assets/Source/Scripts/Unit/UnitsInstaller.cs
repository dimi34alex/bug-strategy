using BugStrategy.Unit.Factory;
using BugStrategy.Unit.UnitSelection;
using Zenject;

namespace BugStrategy.Unit
{
    public class UnitsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
            BindSelector();
            BindHousingReturner();
        }
        
        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<UnitRepository>().FromNew().AsSingle();
        
        private void BindFactory()
        {
            var unitFactory = FindObjectOfType<UnitFactory>(true);
            Container.Bind<UnitFactory>().FromInstance(unitFactory).AsSingle();
        }
        
        private void BindSelector() 
            => Container.Bind<UnitsSelector>().FromNew().AsSingle();

        private void BindHousingReturner() 
            => Container.Bind<HousingBacker>().FromNew().AsSingle().NonLazy();
    }
}