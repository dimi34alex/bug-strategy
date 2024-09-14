using BugStrategy.Constructions.BeeSiegeWeaponsBarrack;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeSiegeWeaponsBarrackFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeSiegeWeaponsBarrackSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.BeeSiegeWeaponsBarrack;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID)
        {
            ConstructionSpawnConfiguration<BeeSiegeWeaponsBarrack.BeeSiegeWeaponsBarrack> configuration = _config.Configuration;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
