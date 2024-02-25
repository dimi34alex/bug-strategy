using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class BeeStoreHouse : EvolvConstruction
    {
        [SerializeField] private BeeStoreHouseConfig config;

        public override ConstructionID ConstructionID => ConstructionID.BeeStoreHouse;

        protected override void OnAwake()
        {
            base.OnAwake();

            levelSystem = new BeeStoreHouseLevelSystem(config.Levels, ref _healthStorage);
        }
    }
}