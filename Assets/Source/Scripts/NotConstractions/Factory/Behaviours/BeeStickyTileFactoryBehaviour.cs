using BugStrategy.NotConstructions.BeeStickyTile;
using UnityEngine;
using Zenject;

namespace BugStrategy.NotConstructions.Factory.Behaviours
{
    public class BeeStickyTileFactoryBehaviour : NotConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeStickyTileSpawnConfig _config;

        public override NotConstructionType NotConstructionType => NotConstructionType.BeeStickyTileConstruction;

        public override TConstruction Create<TConstruction>(NotConstructionID notConstructionID, Transform parent = null)
        {
            NotConstructionSpawnConfiguration<BeeStickyTile.BeeStickyTile> configuration = _config.GetConfiguration();

            TConstruction notConstruction = DiContainer.InstantiatePrefab(configuration.NotConstructionPrefab,
                    configuration.NotConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return notConstruction;
        }
    }
}
