using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.Projectiles.Factory;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Bees
{
    public class Horntail : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private HorntailConfig config;
    
        [Inject] private ProjectilesFactory _projectilesFactory;
    
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Horntail;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HorntailAttackProcessor _attackProcessor;
        
        private AbilitySwordStrike _abilitySwordStrike;
        
        private readonly List<IPassiveAbility> _passiveAbilities = new(1);
        public override IReadOnlyList<IAbility> Abilities => _passiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new FloatStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HorntailAttackProcessor(this, config.AttackRange, config.Damage, config.DamageRadius,
                _cooldownProcessor, _projectilesFactory);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilitySwordStrike = new AbilitySwordStrike(this, _attackProcessor, config.SwordStrikeDamage, 
                config.SwordStrikeDistanceFromCenter, config.SwordStrikeRadius, config.SwordStrikeCooldown, config.SwordStrikeLayers);
            _passiveAbilities.Add(_abilitySwordStrike);        
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new HorntailInternalAi(this, states);
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
            AttackCooldownChanger.Clear();
            _abilitySwordStrike.Reset();

            _stateMachine.SetState(EntityStateID.Idle);
        }

        public HiderCellBase TakeHideCell()
            => new HorntailHiderCell(this, _cooldownProcessor, _abilitySwordStrike);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out HorntailHiderCell hideCell))
            {
                _healthStorage.SetValue(hideCell.HealthPoints.CurrentValue);
                _cooldownProcessor.LoadData(hideCell.AttackCooldown.CurrentTime);
                _abilitySwordStrike.LoadData(hideCell.AbilitySwordStrikeCooldown.CurrentTime);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}