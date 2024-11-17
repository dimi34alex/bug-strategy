using BugStrategy.Constructions;
using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Tiles.WarFog.NewDirectory1
{
    public class ConstructionViewsFactory : FactoryWithId<ConstructionID, ObjectView>
    {
        protected ConstructionViewsFactory(DiContainer diContainer, ConstructionViewsConfig config) 
            : base(diContainer, config, "ConstructionViews") { }
    }
}