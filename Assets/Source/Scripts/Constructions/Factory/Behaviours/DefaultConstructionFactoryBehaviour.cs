﻿using BugStrategy.Constructions.DefaultConstruction;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class DefaultConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly DefaultConstructionConfig _defaultConstructionConfig;

        public override ConstructionType ConstructionType => ConstructionType.TestConstruction;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<DefaultConstruction.DefaultConstruction> configuration = _defaultConstructionConfig.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
