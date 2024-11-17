using BugStrategy.Constructions.ButterflyHouse;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class ButterflyHouseFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly ButterflyHouseSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.ButterflyHouse;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<ButterflyHouse.ButterflyHouse> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
