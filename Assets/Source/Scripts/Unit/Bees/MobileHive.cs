using System.Collections.Generic;
using Projectiles;
using Projectiles.Factory;
using Source.Scripts.Ai.InternalAis;
using Source.Scripts.Ai.UnitAis;
using Source.Scripts.Unit.AbilitiesCore;
using Unit.Bees.Configs;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.Factory;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class MobileHive : BeeUnit, IAttackCooldownChangerEffectable
    {
        [SerializeField] private MobileHiveConfig config;
    
        [Inject] private readonly ProjectileFactory _projectileFactory;
        [Inject] private readonly UnitFactory _unitFactory;
    
        public override UnitType UnitType => UnitType.MobileHive;
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }

        protected override OrderValidatorBase OrderValidator => _warriorOrderValidator;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private WarriorOrderValidator _warriorOrderValidator;
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;

        private AbilityArmorBreakthrough _abilityArmorBreakthrough;
        
        private readonly List<IPassiveAbility> _passiveAbilities = new(1);
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new RangeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor,
                ProjectileType.MobileHiveProjectile, _projectileFactory);
            _warriorOrderValidator = new WarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityArmorBreakthrough = new AbilityArmorBreakthrough(this, config.ExplosionDamage,
                config.ExplosionRadius, config.ExplosionLayers, _unitFactory, config.spawnUnits);
            _passiveAbilities.Add(_abilityArmorBreakthrough);    
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _warriorOrderValidator),
                new AttackState(this, _warriorOrderValidator),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new MobileHiveInternalAi(this, states);
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