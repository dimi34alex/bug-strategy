using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class BeeHouse : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private BeeHouseConfig config;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeHouse;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeHouseLevelSystem(config.Levels, ref resourceRepository, ref _healthStorage);
        }
    }
}