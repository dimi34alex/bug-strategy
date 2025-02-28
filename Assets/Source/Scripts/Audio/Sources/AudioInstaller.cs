using Zenject;

namespace BugStrategy.Audio.Sources
{
    public class AudioInstaller : MonoInstaller
    {
        public override void InstallBindings() 
            => BindFactory();

        private void BindFactory() 
            => Container.Bind<AudioFactory>().FromNew().AsSingle().NonLazy();
    }
}