using BugStrategy.Constructions.BeeTownHall;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeesTownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeTownHallSpawnConfig _config;
    
        public override ConstructionType ConstructionType => ConstructionType.BeeTownHall;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeeTownHall.BeeTownHall> configuration = _config.GetConfiguration();

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}
