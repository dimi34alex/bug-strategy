using BugStrategy.Constructions.BeeLandmine;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeLandmineFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeLandmineSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeLandmine;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeeLandmine.BeeLandmine> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
