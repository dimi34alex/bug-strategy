using System.Collections.Generic;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Wasp : BeeUnit
    {
        [SerializeField] private BeeRangeWarriorConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _warriorOrderValidator;
        public override UnitType UnitType => UnitType.Wasp;

        private WarriorOrderValidatorBase _warriorOrderValidator;
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new RangeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor,
                config.ProjectileType, _projectileFactory);
            _warriorOrderValidator = new RangeWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
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
                new WarriorIdleState(this, _warriorOrderValidator),
                new MoveState(this, _warriorOrderValidator),
                new AttackState(this, _warriorOrderValidator),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
        }
    }
}
