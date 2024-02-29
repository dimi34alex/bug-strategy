using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class BeeStoreHouse : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private BeeStoreHouseConfig config;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeStoreHouse;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeStoreHouseLevelSystem(config.Levels, ref resourceRepository, ref _healthStorage);
        }
    }
}