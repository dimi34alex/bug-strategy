using BugStrategy.Constructions.BeeHouse;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeHouseFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeHouseSpawnConfig _beeHouseSpawnConfig;

        public override ConstructionType ConstructionType => ConstructionType.BeeHouse;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeHouse.BeeHouse> configuration = _beeHouseSpawnConfig.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
