using BugStrategy.Constructions.ButterflyTownHall;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class ButterflyTownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly ButterflyTownHallSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.ButterflyTownHall;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<ButterflyTownHall.ButterflyTownHall> configuration = _config.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
