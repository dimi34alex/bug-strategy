using BugStrategy.NotConstructions;
using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Tiles.WarFog.NewDirectory1
{
    public class NotConstructionViewsFactory : FactoryWithId<NotConstructionID, ObjectView>
    {
        protected NotConstructionViewsFactory(DiContainer diContainer, NotConstructionViewsConfig config) 
            : base(diContainer, config, "ConstructionViews") { }
    }
}