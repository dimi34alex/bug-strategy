using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Unit.Factory;
using BugStrategy.UnitsHideCore;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction
{
    [Serializable]
    public class BeesWaxProduceLevelSystem : ConstructionLevelSystemBase<BeesWaxProduceLevel>
    {
        private readonly ResourceConversionCore _resourceConversionCore;
        private readonly UnitsHider _hider;

        public BeesWaxProduceLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, BeesWaxProduceConfig config, 
            UnitFactory unitFactory, Transform hiderSpawnPosition, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, 
            FloatStorage healthStorage, ref ResourceConversionCore resourceConversionCore, ref UnitsHider hider)
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
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