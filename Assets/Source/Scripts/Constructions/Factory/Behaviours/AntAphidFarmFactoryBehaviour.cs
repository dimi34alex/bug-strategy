using BugStrategy.Constructions.ResourceProduceConstruction.AntAphidFarm;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntAphidFarmFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntAphidFarmSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.AntAphidFarm;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntAphidFarm> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
