using BugStrategy.Constructions.AntHouse;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntHouseFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntHouseSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntHouse;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntHouse.AntHouse> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
