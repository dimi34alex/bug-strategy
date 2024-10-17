using BugStrategy.Constructions.AntWorkerWorkshop;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntWorkerWorkshopFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntWorkerWorkshopSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntWorkerWorkshop;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntWorkerWorkshop.AntWorkerWorkshop> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
