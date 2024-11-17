using BugStrategy.Constructions.AntTownHall;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntTownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntTownHallSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntTownHall;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntTownHall.AntTownHall> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
