using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    public sealed class Hornet : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private HornetConfig config;
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        protected override BeeConfigBase ConfigBase => config;
        public override UnitType UnitType => UnitType.Hornet;

        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HornetAttackProcessor _attackProcessor;

        private AbilityVerifiedBites _abilityVerifiedBites;
        
        private readonly List<IPassiveAbility> _passiveAbilities = new(1);
        public override IReadOnlyList<IAbility> Abilities => _passiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HornetAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityVerifiedBites = new AbilityVerifiedBites(_attackProcessor, config.AbilityCooldown, config.CriticalDamageScale);
            _passiveAbilities.Add(_abilityVerifiedBites);
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new HornetInternalAi(this, states);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _cooldownProcessor.HandleUpdate(time);
            _abilityVerifiedBites.HandleUpdate(time);
        }
        
        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _cooldownProcessor.Reset();
            _abilityVerifiedBites.Reset();
            AttackCooldownChanger.Clear();
            
            _stateMachine.SetState(EntityStateID.Idle);
        }

        public HiderCellBase TakeHideCell()
            => new HornetHiderCell(this, _cooldownProcessor, _abilityVerifiedBites);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out HornetHiderCell hideCell))
            {
                _healthStorage.SetValue(hideCell.HealthPoints.CurrentValue);
                _cooldownProcessor.LoadData(hideCell.AttackCooldown.CurrentTime);
                _abilityVerifiedBites.LoadData(hideCell.AbilityVerifiedBitesCooldown.CurrentTime);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}