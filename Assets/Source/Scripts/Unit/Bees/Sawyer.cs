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
    public sealed class Sawyer : BeeUnit, IAttackCooldownChangeable
    {
        [SerializeField] private SawyerConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Sawyer;

        private WarriorOrderValidator _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private SawyerAttackProcessor _attackProcessor;
        private AbilityRaiseShields _abilityRaiseShields; 
        private float _enterDamageScale = 1;

        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new SawyerAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor,
                config.ProjectileType, _projectileFactory);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
           
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityRaiseShields = new AbilityRaiseShields(this, _attackProcessor, config.RaiseShieldsExistTime,
                config.RaiseShieldsCooldown, config.DamageEnterScale, config.DamageExitScale);
        }
        
        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _cooldownProcessor.HandleUpdate(time);
            _abilityRaiseShields.HandleUpdate(time);
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

        public void SetEnterDamageScale(float enterDamageScale)
            => _enterDamageScale = enterDamageScale;
        
        public override void TakeDamage(IDamageApplicator damageApplicator, float damageScale = 1)
        {
            damageScale *= _enterDamageScale;
            
            base.TakeDamage(damageApplicator, damageScale);
        }

        [ContextMenu(nameof(UseAbility))]
        private void UseAbility()
            => _abilityRaiseShields.Activate();
    }
}