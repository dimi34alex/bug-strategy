using BugStrategy.Constructions.Factory;
using BugStrategy.Tiles.WarFog.NewDirectory1;
using Zenject;

namespace BugStrategy.Constructions
{
    public class ConstructionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
            BindViewsFactory();
            BindSelector();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<ConstructionsRepository>().FromNew().AsSingle();

        private void BindFactory()
        {
            var constructionFactory = FindObjectOfType<ConstructionFactory>(true);
            Container.BindInterfacesAndSelfTo<ConstructionFactory>().FromInstance(constructionFactory).AsSingle();
        }
        
        private void BindViewsFactory() 
            => Container.BindInterfacesAndSelfTo<ConstructionViewsFactory>().FromNew().AsSingle();
        
        private void BindSelector()
            => Container.BindInterfacesAndSelfTo<ConstructionSelector>().FromNew().AsSingle();
    }
}