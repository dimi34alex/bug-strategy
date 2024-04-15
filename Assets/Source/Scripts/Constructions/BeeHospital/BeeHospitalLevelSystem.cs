using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;

namespace Constructions.BeeHospital
{
    public class BeeHospitalLevelSystem : ConstructionLevelSystemBase<BeeHospitalLevel>
    {
        private readonly UnitsHider _hider;
        private readonly HealProcessor _healProcessor;

        public BeeHospitalLevelSystem(BeeHospitalConfig config, Transform spawnPosition, 
            UnitFactory unitFactory, ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage, 
            ref UnitsHider hider, ref HealProcessor healProcessor) 
            : base(config.Levels, ref resourceRepository, ref healthStorage)
        {
            _hider = hider = new UnitsHider(CurrentLevel.HiderCapacity ,unitFactory , spawnPosition, config.HiderAccess);
            _healProcessor = healProcessor = new HealProcessor(hider, CurrentLevel.HealPerSecond);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _hider.SetCapacity(CurrentLevel.HiderCapacity);
            _healProcessor.SetHealPerSecond(CurrentLevel.HealPerSecond);
        }
    }
}