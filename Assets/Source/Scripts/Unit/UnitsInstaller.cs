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
            BindPlayerSelector();
            BindEnemySelector();
            BindHousingReturner();
        }
        
        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<UnitRepository>().FromNew().AsSingle();
        
        private void BindFactory()
        {
            var unitFactory = FindObjectOfType<UnitFactory>(true);
            Container.Bind<UnitFactory>().FromInstance(unitFactory).AsSingle();
        }
        
        private void BindPlayerSelector() 
            => Container.Bind<PlayerUnitsSelector>().FromNew().AsSingle();

        private void BindEnemySelector() 
            => Container.Bind<EnemyUnitsSelector>().FromNew().AsSingle();
        
        private void BindHousingReturner() 
            => Container.Bind<HousingBacker>().FromNew().AsSingle().NonLazy();
    }
}