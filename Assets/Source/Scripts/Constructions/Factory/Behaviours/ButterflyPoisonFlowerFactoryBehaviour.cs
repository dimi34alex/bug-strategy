using BugStrategy.Constructions.ButterflyPoisonFlower;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class ButterflyPoisonFlowerFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly ButterflyPoisonFlowerSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.ButterflyPoisonFlower;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<ButterflyPoisonFlower.ButterflyPoisonFlower> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
