using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class ButterflyStoreHouse : EvolvConstruction
    {
        [SerializeField] private ButterflyStoreHouseConfig config;
    
        public override ConstructionID ConstructionID => ConstructionID.ButterflyStoreHouse;

        protected override void OnAwake()
        {
            base.OnAwake();

            levelSystem = new ButterflyStoreHouseLevelSystem(config.Levels, ref _healthStorage);
        }
    }   
}
