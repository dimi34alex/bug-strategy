using BugStrategy.Constructions.BeeMercenaryBarrack;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeMercenaryBarrackFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeMercenaryBarrackSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeMercenaryBarrack;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeeMercenaryBarrack.BeeMercenaryBarrack> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}

