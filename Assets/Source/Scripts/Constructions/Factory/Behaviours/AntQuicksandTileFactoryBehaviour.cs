using BugStrategy.Constructions.AntQuicksandTile;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntQuicksandTileFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntQuicksandTileSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.AntQuicksandTile;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<AntQuicksandTile.AntQuicksandTile> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
