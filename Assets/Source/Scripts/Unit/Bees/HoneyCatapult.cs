using System.Collections.Generic;
using Projectiles.Factory;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees.Configs;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class HoneyCatapult : BeeUnit, IAttackCooldownChangerEffectable
    {
        [SerializeField] private HoneyCatapultConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        public override UnitType UnitType => UnitType.HoneyCatapult;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

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
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new HoneyCatapultInternalAi(this, states);
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
            AttackCooldownChanger.Clear();
            
            _stateMachine.SetState(EntityStateID.Idle);
        }
    }
}