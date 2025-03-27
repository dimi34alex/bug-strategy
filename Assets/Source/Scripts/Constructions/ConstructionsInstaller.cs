using BugStrategy.Constructions.Factory;
using Zenject;

namespace BugStrategy.Constructions
{
    public class ConstructionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
            BindSelector();
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<ConstructionsRepository>().FromNew().AsSingle();

        private void BindFactory()
        {
            var constructionFactory = FindObjectOfType<ConstructionFactory>(true);
            Container.BindInterfacesAndSelfTo<ConstructionFactory>().FromInstance(constructionFactory).AsSingle();
        }
        
        private void BindSelector()
            => Container.BindInterfacesAndSelfTo<ConstructionSelector>().FromNew().AsSingle();
    }
}