using System.Collections.Generic;
using AttackCooldownChangerSystem;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Bumblebee : BeeUnit, IAttackCooldownChangeable
    {
        [SerializeField] private BumblebeeConfig config;
        
        [Inject] private readonly IConstructionFactory _constructionFactory;

        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override UnitType UnitType => UnitType.Bumblebee;
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private WarriorOrderValidator _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private MeleeAttackProcessor _attackProcessor;

        private AbilityAccumulation _abilityAccumulation;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityAccumulation = new AbilityAccumulation(this, config.ExplosionRadius, config.ExplosionDamage, config.ExplosionLayers, _constructionFactory);
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
