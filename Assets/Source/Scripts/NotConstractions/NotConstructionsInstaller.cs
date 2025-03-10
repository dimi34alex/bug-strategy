using BugStrategy.NotConstructions.Factory;
using BugStrategy.Tiles.WarFog.NewDirectory1;
using Zenject;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
            BindViewsFactory();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<NotConstructionsRepository>().FromNew().AsSingle();

        private void BindFactory()
        {
            var constructionFactory = FindObjectOfType<NotConstructionFactory>(true);
            Container.BindInterfacesAndSelfTo<NotConstructionFactory>().FromInstance(constructionFactory).AsSingle();
        }
        
        private void BindViewsFactory() 
            => Container.BindInterfacesAndSelfTo<NotConstructionViewsFactory>().FromNew().AsSingle();
    }
}