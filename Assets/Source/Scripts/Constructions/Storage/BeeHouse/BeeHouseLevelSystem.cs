using System;
using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeeHouseLevelSystem : ConstructionLevelSystemBase<BeeHouseLevel>
    {
        private readonly UnitsHider _hider;

        public BeeHouseLevelSystem(BeeHouseConfig config, Transform hiderSpawnPosition, UnitFactory unitFactory, 
            ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage, ref UnitsHider hider)
            : base(config.Levels, ref resourceRepository, ref healthStorage)
        {
            _hider = hider = new UnitsHider(CurrentLevel.HiderCapacity ,unitFactory , hiderSpawnPosition, config.HiderAccess);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}