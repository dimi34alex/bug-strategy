using BugStrategy.Constructions.BeeStorage;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeStorageFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeStorageSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.BeeStorage;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeStorage.BeeStorage> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
