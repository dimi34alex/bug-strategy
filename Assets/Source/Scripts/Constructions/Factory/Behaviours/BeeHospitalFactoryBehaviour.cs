using BugStrategy.Constructions.BeeHospital;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory.Behaviours
{
    public class BeeHospitalFactoryBehaviour : ConstructionFactoryBehaviourBase
    {
        [Inject] private readonly BeeHospitalSpawnConfig _config;

        public override ConstructionType ConstructionType => ConstructionType.BeeHospital;

        public override TConstruction Create<TConstruction>(ConstructionID constructionID, Transform parent = null)
        {
            ConstructionSpawnConfiguration<BeeHospital.BeeHospital> configuration = _config.Config;

            TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                    configuration.ConstructionPrefab.transform.position, configuration.Rotation, parent)
                .GetComponent<TConstruction>();
        
            return construction;
        }
    }
}

