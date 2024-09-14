using BugStrategy.Constructions.ButterflyPoisonFlower;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class ButterflyPoisonFlowerFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly ButterflyPoisonFlowerSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.ButterflyPoisonFlower;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<ButterflyPoisonFlower.ButterflyPoisonFlower> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
