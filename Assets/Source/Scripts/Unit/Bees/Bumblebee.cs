using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Source.Scripts.Unit.AbilitiesCore;
using Unit.Bees.Configs;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Bumblebee : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private BumblebeeConfig config;
        
        [Inject] private readonly IConstructionFactory _constructionFactory;

        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }
        public override UnitType UnitType => UnitType.Bumblebee;
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private MeleeAttackProcessor _attackProcessor;

        private AbilityAccumulation _abilityAccumulation;

        private readonly List<IPassiveAbility> _passiveAbilities = new(1);
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities => _passiveAbilities;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityAccumulation = new AbilityAccumulation(this, config.ExplosionRadius, config.ExplosionDamage, config.ExplosionLayers, _constructionFactory);
            _passiveAbilities.Add(_abilityAccumulation);
            
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new BumblebeeInternalAi(this, states);
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
            => new BumblebeeHiderCell(this, _cooldownProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out BumblebeeHiderCell hideCell))
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
