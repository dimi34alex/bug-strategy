using System.Collections.Generic;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;
using Unit.States;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class Hornet : BeeUnit
    {
        [SerializeField] private HornetConfig config;
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Hornet;

        private WarriorOrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new MeleeWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _cooldownProcessor.HandleUpdate(time);
        }
        
        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _cooldownProcessor.Reset();
        
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _orderValidator),
                new MoveState(this, _orderValidator),
                new AttackState(this, _orderValidator),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
        }
    }
}