using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    public class TileShadowFactory : FactorySinglePool<TileShadow>
    {
        protected TileShadowFactory(DiContainer diContainer, TilesShadowerConfig config) 
            : base(diContainer, config.TileShadowPrefab, "TilesShadows") { }
    }
}