using System.Collections.Generic;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.Effects.InnerProcessors;
using Unit.Effects.Interfaces;
using Unit.OrderValidatorCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Horntail : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private HorntailConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        public override UnitType UnitType => UnitType.Horntail;

        private OrderValidatorBase _orderValidator;
        private CooldownProcessor _cooldownProcessor;
        private HorntailAttackProcessor _attackProcessor;
        private AbilitySwordStrike _abilitySwordStrike;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new HorntailAttackProcessor(this, config.AttackRange, config.Damage, config.DamageRadius,
                _cooldownProcessor, _projectileFactory);
            _orderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);

            _abilitySwordStrike = new AbilitySwordStrike(this, _attackProcessor, config.SwordStrikeDamage, 
                config.SwordStrikeDistanceFromCenter, config.SwordStrikeRadius, config.SwordStrikeCooldown, config.SwordStrikeLayers);
                    
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