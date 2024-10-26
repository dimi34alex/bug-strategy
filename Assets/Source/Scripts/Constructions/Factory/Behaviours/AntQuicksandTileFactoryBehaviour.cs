using BugStrategy.Constructions.AntQuicksandTile;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class AntQuicksandTileFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly AntQuicksandTileSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.AntQuicksandTile;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<AntQuicksandTile.AntQuicksandTile> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
