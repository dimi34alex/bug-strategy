using BugStrategy.Constructions.AntTownHall;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntTownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntTownHallSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntTownHall;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntTownHall.AntTownHall> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
