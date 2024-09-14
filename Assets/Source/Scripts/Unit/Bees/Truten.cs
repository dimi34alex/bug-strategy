using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.Trigger;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    public class Truten : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private TrutenConfig config;
        [SerializeField] private SphereTrigger abilityStandardBearerZone;
        
        public override UnitType UnitType => UnitType.Truten;
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }

        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private MeleeAttackProcessor _attackProcessor;
        
        private AbilityStandardBearer _abilityStandardBearer;
        private AbilityBraveDeath _abilityBraveDeath;
        
        private readonly List<IPassiveAbility> _passiveAbilities = new(2);
        public override IReadOnlyList<IAbility> Abilities => _passiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new FloatStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);

            _abilityStandardBearer = new AbilityStandardBearer(this, abilityStandardBearerZone, config.StandardBearerRadius);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);
            _abilityBraveDeath = new AbilityBraveDeath(this, config.HealValue, config.HealRadius, config.HealLayers);
                
            _passiveAbilities.Add(_abilityBraveDeath);
            _passiveAbilities.Add(_abilityStandardBearer);
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new TrutenInternalAi(this, states);
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

        public HiderCellBase TakeHideCell()
            => new TrutenHiderCell(this, _cooldownProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out TrutenHiderCell hideCell))
            {
                _healthStorage.SetValue(hideCell.HealthPoints.CurrentValue);
                _cooldownProcessor.LoadData(hideCell.CooldownValue.CurrentTime);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}