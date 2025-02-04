﻿using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Constructions.AntQuicksandTile
{
    public class AntQuicksandTile : ConstructionBase
    {
        [SerializeField] private AntQuicksandTileConfig config;
        [SerializeField] private TriggerBehaviour triggerBehaviour;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntQuicksandTile;
        protected override ConstructionConfigBase ConfigBase => config;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _healthStorage.SetCapacity(config.HealthPoints);
            _healthStorage.SetValue(config.HealthPoints);
        }

        protected override void OnStart()
        {
            triggerBehaviour.EnterEvent += OnUnitEnter;
            triggerBehaviour.ExitEvent += OnUnitExit;
        }

        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                
            }
        }

        private void OnUnitExit(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                
            }
        }
    }
}