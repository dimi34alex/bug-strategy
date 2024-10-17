using BugStrategy.Constructions.DefaultConstruction;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class DefaultConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly DefaultConstructionConfig _defaultConstructionConfig;

        public override ConstructionType ConstructionType => ConstructionType.TestConstruction;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<DefaultConstruction.DefaultConstruction> configuration = _defaultConstructionConfig.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
