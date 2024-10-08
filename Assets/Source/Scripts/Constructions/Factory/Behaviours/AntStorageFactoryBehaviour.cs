using BugStrategy.Constructions.AntStorage;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntStorageFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntStorageSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntStorage;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntStorage.AntStorage> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
