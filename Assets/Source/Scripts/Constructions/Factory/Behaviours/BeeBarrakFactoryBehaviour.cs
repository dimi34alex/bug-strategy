using BugStrategy.Constructions.BeeBarrack;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeBarrakFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeBarrackSpawnConfig _barrackConfig;

        public override ConstructionType ConstructionType => ConstructionType.BeeBarrack;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeBarrack.BeeBarrack> configuration = _barrackConfig.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}

