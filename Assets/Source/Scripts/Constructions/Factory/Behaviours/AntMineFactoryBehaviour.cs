using BugStrategy.Constructions.ResourceProduceConstruction.AntMine;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntMineFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntMineSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntMine;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntMine> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
