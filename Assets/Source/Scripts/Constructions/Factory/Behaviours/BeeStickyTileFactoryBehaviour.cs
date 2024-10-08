﻿using BugStrategy.Constructions.BeeStickyTile;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeStickyTileFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeStickyTileSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeStickyTileConstruction;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeStickyTile.BeeStickyTile> configuration = _config.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
