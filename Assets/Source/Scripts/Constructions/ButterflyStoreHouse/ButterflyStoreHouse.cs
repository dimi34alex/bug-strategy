using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class ButterflyStoreHouse : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private ButterflyStoreHouseConfig config;
    
        public override AffiliationEnum Affiliation => AffiliationEnum.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyStoreHouse;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new ButterflyStoreHouseLevelSystem(config.Levels, ref resourceRepository, ref _healthStorage);
        }
    }   
}
