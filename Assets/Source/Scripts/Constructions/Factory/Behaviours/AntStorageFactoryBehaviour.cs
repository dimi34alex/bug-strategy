using BugStrategy.Constructions.AntStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntStorageFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntStorageSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntStorage;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntStorage.AntStorage> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
