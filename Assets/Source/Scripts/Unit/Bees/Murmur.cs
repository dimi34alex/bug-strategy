using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.Effects;
using BugStrategy.EntityState;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Bees
{
    public sealed class Murmur : BeeUnit, IAttackCooldownChangerEffectable, IHidableUnit
    {
        [SerializeField] private MurmurConfig config;
        [SerializeField] private GameObject resourceSkin;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public override UnitType UnitType => UnitType.Murmur;
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        protected override BeeConfigBase ConfigBase => config;
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;

        private ResourceExtractionProcessor _resourceExtractionProcessor;
        private MurmurOrderValidator _orderValidator;
        private AttackProcessorBase _attackProcessor;
        private CooldownProcessor _cooldownProcessor;

        public override IReadOnlyList<IAbility> Abilities => ActiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities { get; } = new List<IPassiveAbility>();
        
        public AttackCooldownChanger AttackCooldownChanger { get; private set; }
        public override InternalAiBase InternalAi { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            _resourceExtractionProcessor = new ResourceExtractionProcessor(this, config.GatheringCapacity, config.GatheringTime,
                _teamsResourcesGlobalStorage, resourceSkin);
            _cooldownProcessor = new CooldownProcessor(config.AttackCooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.AttackDamage, _cooldownProcessor);
            _orderValidator = new MurmurOrderValidator(this, config.InteractionRange, _attackProcessor, _resourceExtractionProcessor);
            AttackCooldownChanger = new AttackCooldownChanger(_cooldownProcessor);
        
            var states = new List<EntityStateBase>()
            {
                new IdleState(),
                new MoveState(this, _orderValidator),
                new ResourceExtractionState(this, _resourceExtractionProcessor),
                new StorageResourceState(this, _resourceExtractionProcessor),
                new AttackState(this, _attackProcessor, _cooldownProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);

            InternalAi = new MurmurInternalAi(this, states);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _resourceExtractionProcessor.HandleUpdate(time);
            _cooldownProcessor.HandleUpdate(time);
        }
        
        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _resourceExtractionProcessor.Reset();
            _cooldownProcessor.Reset();
            AttackCooldownChanger.Clear();
            
            _stateMachine.SetState(EntityStateID.Idle);
        }

        public HiderCellBase TakeHideCell()
            => new MurmurHiderCell(this, _resourceExtractionProcessor, _cooldownProcessor);
        
        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out MurmurHiderCell hideCell))
            {
                _healthStorage.SetValue(hideCell.HealthPoints.CurrentValue);
                _resourceExtractionProcessor.LoadData(hideCell.ResourceExtracted, hideCell.ExtractedResourceID);
                _cooldownProcessor.LoadData(hideCell.CooldownValue.CurrentTime);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}