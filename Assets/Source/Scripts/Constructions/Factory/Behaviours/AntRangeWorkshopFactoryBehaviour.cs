using BugStrategy.Constructions.AntRangeWorkshop;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntRangeWorkshopFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntRangeWorkshopSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntRangeWorkshop;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntRangeWorkshop.AntRangeWorkshop> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
