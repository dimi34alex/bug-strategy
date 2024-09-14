using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.Projectiles.Factory;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Bees
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
        
        private readonly List<IPassiveAbility> _passiveAbilities = new(1);
        public override IReadOnlyList<IAbility> Abilities => _passiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new FloatStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HoneyCatapultAttackProcessor(this, config.AttackRange, config.Damage, 
                config.DamageRadius, _cooldownProcessor, _projectileFactory);
            _orderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityArtillerySalvo = new AbilityArtillerySalvo(_attackProcessor, config.ConstructionDamageScale);
            _abilityStickyProjectiles = new AbilityStickyProjectiles(_attackProcessor, config.StickyProjectileNum);
            _passiveAbilities.Add(_abilityArtillerySalvo);
            _passiveAbilities.Add(_abilityStickyProjectiles);
                
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