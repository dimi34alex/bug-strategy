using BugStrategy.Constructions.AntMeleeWorkshop;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntMeleeWorkshopFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntMeleeWorkshopSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntMeleeWorkshop;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntMeleeWorkshop.AntMeleeWorkshop> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
