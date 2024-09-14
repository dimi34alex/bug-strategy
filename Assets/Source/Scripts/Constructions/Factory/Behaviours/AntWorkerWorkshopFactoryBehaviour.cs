using BugStrategy.Constructions.AntWorkerWorkshop;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntWorkerWorkshopFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntWorkerWorkshopSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntWorkerWorkshop;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntWorkerWorkshop.AntWorkerWorkshop> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
