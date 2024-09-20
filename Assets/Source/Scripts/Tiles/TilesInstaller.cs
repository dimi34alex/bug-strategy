using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
            BindRepository();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<TilesFactory>().FromNew().AsSingle();
        }
        
        private void BindRepository()
        {
            Container.BindInterfacesAndSelfTo<TilesRepository>().FromNew().AsSingle();
        }
    }
}