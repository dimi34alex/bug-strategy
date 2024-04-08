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
    public class Horntail : BeeUnit, IAttackCooldownChangeable
    {
        [SerializeField] private HorntailConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Horntail;

        private WarriorOrderValidator _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HorntailAttackProcessor _attackProcessor;
        private AbilitySwordStrike _abilitySwordStrike;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HorntailAttackProcessor(this, config.AttackRange, config.Damage, config.DamageRadius,
                _cooldownProcessor, _projectileFactory);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilitySwordStrike = new AbilitySwordStrike(this, _attackProcessor, config.SwordStrikeDamage, 
                config.SwordStrikeDistanceFromCenter, config.SwordStrikeRadius, config.SwordStrikeCooldown, config.SwordStrikeLayers);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _cooldownProcessor.HandleUpdate(time);
            _abilitySwordStrike.HandleUpdate(time);
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