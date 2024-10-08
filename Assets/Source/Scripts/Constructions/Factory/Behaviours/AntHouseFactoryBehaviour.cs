using BugStrategy.Constructions.AntHouse;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntHouseFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntHouseSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntHouse;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntHouse.AntHouse> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
