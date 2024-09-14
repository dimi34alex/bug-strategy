using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.UnitsHideCore;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHouse
{
    [Serializable]
    public class BeeHouseLevelSystem : ConstructionLevelSystemBase<BeeHouseLevel>
    {
        private readonly UnitsHider _hider;

        public BeeHouseLevelSystem(ConstructionBase construction, BeeHouseConfig config, Transform hiderSpawnPosition,
            UnitFactory unitFactory, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, 
            ref UnitsHider hider)
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _hider = hider = new UnitsHider(construction, CurrentLevel.HiderCapacity ,unitFactory , hiderSpawnPosition, config.HiderAccess);
        }
        
        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}