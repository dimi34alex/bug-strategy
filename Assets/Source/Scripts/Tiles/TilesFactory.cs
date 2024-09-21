using BugStrategy.Factory;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesFactory : ObjectsFactoryBase<int, Tile>
    {
        public TilesFactory(DiContainer diContainer, TilesConfig config) 
            : base(diContainer, config, "Tiles") { }
    }
}