using System.Collections.Generic;
using Source.Scripts.Unit.Bees.HiderCells;
using Unit.Bees.Configs;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;

namespace Unit.Bees
{
    public class Truten : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private TrutenConfig config;
        [SerializeField] private SphereTrigger abilityStandardBearerZone;
        
        public override UnitType UnitType => UnitType.Truten;
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }

        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private MeleeAttackProcessor _attackProcessor;
        
        private AbilityStandardBearer _abilityStandardBearer;
        private AbilityBraveDeath _abilityBraveDeath;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);

            _abilityStandardBearer = new AbilityStandardBearer(this, abilityStandardBearerZone, config.StandardBearerRadius);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);
            _abilityBraveDeath = new AbilityBraveDeath(this, config.HealValue, config.HealRadius, config.HealLayers);
                
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _orderValidator),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
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