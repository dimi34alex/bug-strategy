using BugStrategy.Constructions.BeeWaxTower;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeWaxTowerFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeWaxTowerSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.BeeWaxTower;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeWaxTower.BeeWaxTower> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
