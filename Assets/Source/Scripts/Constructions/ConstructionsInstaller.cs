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
        }

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<ConstructionsRepository>().FromNew().AsSingle();

        private void BindFactory()
        {
            var constructionFactory = FindObjectOfType<ConstructionFactory>(true);
            Container.BindInterfacesAndSelfTo<ConstructionFactory>().FromInstance(constructionFactory).AsSingle();
        }
    }
}