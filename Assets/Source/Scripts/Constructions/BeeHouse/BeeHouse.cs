using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class BeeHouse : EvolvConstruction
    {
        [SerializeField] private BeeHouseEvolveConfig config;

        public override ConstructionID ConstructionID => ConstructionID.BeeHouse;

        protected override void OnAwake()
        {
            base.OnAwake();

            levelSystem = new BeeHouseLevelSystem(config.Levels, ref _healthStorage);
        }
    }
}