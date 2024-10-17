using BugStrategy.Constructions.BeeStickyTile;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeStickyTileFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeStickyTileSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeStickyTileConstruction;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeeStickyTile.BeeStickyTile> configuration = _config.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
