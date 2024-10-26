using BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeesWaxProduceConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeesWaxProduceConstructionSpawnConfig _beesWaxProduceConstructionConfig;

        public override ConstructionType ConstructionType => ConstructionType.BeeWaxProduceConstruction;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeesWaxProduceConstruction> configuration = _beesWaxProduceConstructionConfig.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
