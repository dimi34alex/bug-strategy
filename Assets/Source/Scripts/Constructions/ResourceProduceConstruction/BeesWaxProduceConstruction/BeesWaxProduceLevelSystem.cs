using System;
using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class BeesWaxProduceLevelSystem : ConstructionLevelSystemBase<BeesWaxProduceLevel>
    {
        private readonly ResourceConversionCore _resourceConversionCore;
        private readonly UnitsHider _hider;

        public BeesWaxProduceLevelSystem(BeesWaxProduceConfig config, UnitFactory unitFactory, Transform hiderSpawnPosition,
            ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage, 
            ref ResourceConversionCore resourceConversionCore, ref UnitsHider hider)
            : base(config.Levels, ref resourceRepository, ref healthStorage)
        {
            _resourceConversionCore = resourceConversionCore =
                new ResourceConversionCore(CurrentLevel.ResourceConversionProccessInfo);
            
            _hider = hider = new UnitsHider(CurrentLevel.HiderCapacity ,unitFactory , hiderSpawnPosition, config.HiderAccess);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}