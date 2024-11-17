using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesFactory : FactoryWithId<int, Tile>
    {
        public TilesFactory(DiContainer diContainer, TilesConfig config) 
            : base(diContainer, config, "Tiles") { }
    }
}