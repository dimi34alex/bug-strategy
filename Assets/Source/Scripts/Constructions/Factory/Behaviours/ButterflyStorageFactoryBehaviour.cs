using BugStrategy.Constructions.ButterflyStorage;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class ButterflyStorageFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly ButterflyStorageSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.ButterflyStorage;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<ButterflyStorage.ButterflyStorage> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
