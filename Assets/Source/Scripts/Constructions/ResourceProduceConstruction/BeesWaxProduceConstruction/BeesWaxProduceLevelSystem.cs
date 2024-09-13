using System;
using Constructions.LevelSystemCore;
using Source.Scripts;
using Source.Scripts.ResourcesSystem;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;
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

        public BeesWaxProduceLevelSystem(ConstructionBase construction, BeesWaxProduceConfig config, 
            UnitFactory unitFactory, Transform hiderSpawnPosition, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, 
            FloatStorage healthStorage, ref ResourceConversionCore resourceConversionCore, ref UnitsHider hider)
            : base(construction, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _resourceConversionCore = resourceConversionCore =
                new ResourceConversionCore(CurrentLevel.ResourceConversionProccessInfo);
            
            _hider = hider = new UnitsHider(construction, CurrentLevel.HiderCapacity ,unitFactory , hiderSpawnPosition, config.HiderAccess);
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}