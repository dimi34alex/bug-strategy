using System.Collections.Generic;
using AttackCooldownChangerSystem;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class Hornet : BeeUnit, IAttackCooldownChangeable, IHidableUnit
    {
        [SerializeField] private HornetConfig config;
        
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Hornet;

        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        
        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HornetAttackProcessor _attackProcessor;

        private AbilityVerifiedBites _abilityVerifiedBites;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HornetAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilityVerifiedBites =
                new AbilityVerifiedBites(_attackProcessor, config.AbilityCooldown, config.CriticalDamageScale);
            
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
            _abilityVerifiedBites.HandleUpdate(time);
        }
        
        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _cooldownProcessor.Reset();
            _abilityVerifiedBites.Reset();
            AttackCooldownChanger.Reset();
            
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