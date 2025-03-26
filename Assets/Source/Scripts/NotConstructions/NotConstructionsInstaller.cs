using BugStrategy.NotConstructions.Factory;
using Zenject;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionsInstaller : MonoInstaller
    {
        public override void InstallBindings ()
        {
            BindRepository();
            BindFactory();
        }

        private void BindRepository ()
            => Container.BindInterfacesAndSelfTo<NotConstructionsGrid>().FromNew().AsSingle();

        private void BindFactory ()
        {
            var constructionFactory = FindObjectOfType<NotConstructionFactory>(true);
            Container.BindInterfacesAndSelfTo<NotConstructionFactory>().FromInstance(constructionFactory).AsSingle();
        }

    }
}