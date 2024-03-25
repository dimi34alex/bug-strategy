using System.Collections.Generic;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public sealed class Sawyer : BeeUnit
    {
        [SerializeField] private SawyerConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Sawyer;

        private WarriorOrderValidator _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new RangeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor,
                config.ProjectileType, _projectileFactory);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
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