using System.Collections.Generic;
using AttackCooldownChangerSystem;
using Constructions.UnitsHideConstruction.Cells.BeesHiderCells;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Wasp : BeeUnit, IAttackCooldownChangeable, IHidableUnit
    {
        [SerializeField] private BeeRangeWarriorConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override OrderValidatorBase OrderValidator => _warriorOrderValidator;
        public override UnitType UnitType => UnitType.Wasp;

        private OrderValidatorBase _warriorOrderValidator;
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _cooldownProcessor = new CooldownProcessor(config.Cooldown);
            _attackProcessor = new RangeAttackProcessor(this, config.AttackRange, config.Damage, _cooldownProcessor,
                config.ProjectileType, _projectileFactory);
            _warriorOrderValidator = new HidableWarriorOrderValidator(this, config.InteractionRange, _cooldownProcessor, _attackProcessor);
            
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);
                    
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _attackProcessor),
                new MoveState(this, _warriorOrderValidator),
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
            AttackCooldownChanger.Reset();

            _stateMachine.SetState(EntityStateID.Idle);
        }

        public HiderCellBase TakeHideCell()
            => new WaspHiderCell(this, _cooldownProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out WaspHiderCell waspHiderCell))
            {
                _healthStorage.SetValue(waspHiderCell.HealthPoints.CurrentValue);
                _cooldownProcessor.LoadData(waspHiderCell.CooldownValue.CurrentTime);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}
