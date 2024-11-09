using Zenject;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    public class TilesShadowerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindShadower();
            BindFactory();
        }

        private void BindShadower() 
            => Container.BindInterfacesAndSelfTo<TilesShadower>().FromNew().AsSingle();
        
        private void BindFactory() 
            => Container.BindInterfacesAndSelfTo<TileShadowFactory>().FromNew().AsSingle();
    }
}