using BugStrategy.Constructions.BeeLandmine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeLandmineFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeLandmineSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeLandmine;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeLandmine.BeeLandmine> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
