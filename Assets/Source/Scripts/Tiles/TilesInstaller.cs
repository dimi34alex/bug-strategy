using BugStrategy.Tiles.WarFog;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
            BindRepository();
            BindVisibleModificator();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<TilesFactory>().FromNew().AsSingle();
        }
        
        private void BindRepository()
        {
            Container.BindInterfacesAndSelfTo<TilesRepository>().FromNew().AsSingle();
        }

        private void BindVisibleModificator()
        {
            _tileFogVisibilityModificator = new TileFogVisibilityModificator(true);
            Container.BindInterfacesAndSelfTo<TileFogVisibilityModificator>().FromInstance(_tileFogVisibilityModificator).AsSingle();
        }

        //TODO: remove this temporary code
        private TileFogVisibilityModificator _tileFogVisibilityModificator;
        [ContextMenu("Toggle fog visibility")]
        private void ToggleFogVisibility() 
            => _tileFogVisibilityModificator.SetState(!_tileFogVisibilityModificator.FogIsVisible);
    }
}