using System.Collections.Generic;
using AttackCooldownChangerSystem;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class HoneyCatapult : BeeUnit, IAttackCooldownChangeable
    {
        [SerializeField] private HoneyCatapultConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        public override UnitType UnitType => UnitType.HoneyСatapult;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HoneyCatapultAttackProcessor _attackProcessor;

        private AbilityArtillerySalvo _abilityArtillerySalvo;
        private AbilityStickyProjectiles _abilityStickyProjectiles;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HoneyCatapultAttackProcessor(this, config.AttackRange, config.Damage, 
                config.DamageRadius, _cooldownProcessor, _projectileFactory);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityArtillerySalvo = new AbilityArtillerySalvo(_attackProcessor, config.ConstructionDamageScale);
            _abilityStickyProjectiles = new AbilityStickyProjectiles(_attackProcessor, config.StickyProjectileNum);
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
            AttackCooldownChanger.Reset();

            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
        }
    }
}