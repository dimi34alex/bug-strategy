using BugStrategy.PoisonFog.Factory;
using Zenject;

namespace BugStrategy.PoisonFog
{
    public class PoisonFogInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
            BindFactory();
        }

        private void BindRepository()
        {
            Container.BindInterfacesAndSelfTo<PoisonFogsRepository>().FromNew().AsSingle();
        }

        private void BindFactory()
        {
            var poisonFogFactory = FindObjectOfType<PoisonFogFactory>();
            Container.BindInterfacesAndSelfTo<PoisonFogFactory>().FromInstance(poisonFogFactory).AsSingle();
        }
    }
}