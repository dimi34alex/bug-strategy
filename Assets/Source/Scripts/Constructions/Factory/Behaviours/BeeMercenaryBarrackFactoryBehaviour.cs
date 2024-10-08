using BugStrategy.Constructions.BeeMercenaryBarrack;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeMercenaryBarrackFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeMercenaryBarrackSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeMercenaryBarrack;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeMercenaryBarrack.BeeMercenaryBarrack> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}

