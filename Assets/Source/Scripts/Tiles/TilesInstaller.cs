using BugStrategy.Tiles.WarFog;
using BugStrategy.Tiles.WarFog.NewDirectory1;
using BugStrategy.Tiles.WarFog.Shadows;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesInstaller : MonoInstaller
    {
        [SerializeField] private bool showContentOnBecameTileFree;
        
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [SerializeField, Header("worked only in editor and dev build")] private bool fogIsVisible = true;
#endif
        
        public override void InstallBindings()
        {
            BindFactory();
            BindRepository();
            BindContentHider();
            BindVisibleModificator();
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            _tileFogVisibilityModificator.SetState(fogIsVisible);
#endif         
        }

        private void BindFactory() 
            => Container.BindInterfacesAndSelfTo<TilesFactory>().FromNew().AsSingle();

        private void BindRepository() 
            => Container.BindInterfacesAndSelfTo<TilesRepository>().FromNew().AsSingle();

        private void BindContentHider() 
            => Container.BindInterfacesAndSelfTo<TilesContentHider>().FromNew().AsSingle().WithArguments(showContentOnBecameTileFree);
        
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