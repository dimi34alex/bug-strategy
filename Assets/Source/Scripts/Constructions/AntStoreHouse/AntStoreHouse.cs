using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class AntStoreHouse : EvolvConstruction
    {
        [SerializeField] private AntStoreHouseConfig config;

        public override ConstructionID ConstructionID => ConstructionID.AntStoreHouse;

        protected override void OnAwake()
        {
            base.OnAwake();

            levelSystem = new AntStoreHouseLevelSystem(config.Levels, ref _healthStorage);
        }
    }
}