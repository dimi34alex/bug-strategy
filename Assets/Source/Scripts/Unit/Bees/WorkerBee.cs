using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.EntityState;
using BugStrategy.Missions;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Selection;
using BugStrategy.TechnologiesSystem;
using BugStrategy.TechnologiesSystem.Technologies;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.Bees.WorkerBeeAttack;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.Unit.Repairing;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Bees
{
    public class WorkerBee : BeeUnit, IHidableUnit
    {
        [SerializeField] private WorkerBeeConfig config;
        [SerializeField] private GameObject resourceSkin;

        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;
        [Inject] private readonly Selector _selector;
        
        public override UnitType UnitType => UnitType.WorkerBee;
        protected override OrderValidatorBase OrderValidator => _orderValidator;
        protected override BeeConfigBase ConfigBase => config;

        private RepairAbility _repairAbility;
        private HiveProtectionAbility _hiveProtectionAbility;
        private readonly List<IActiveAbility> _activeAbilities = new (1);
        
        private CooldownProcessor _cooldownProcessor;
        private AttackProcessorBase _attackProcessor;

        private OrderValidatorBase _orderValidator;
        private RepairProcessor _repairProcessor;
        private ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public IReadOnlyAttackProcessor AttackProcessor => _attackProcessor;
        public IReadOnlyResourceExtractionProcessor ResourceExtractionProcessor => _resourceExtractionProcessor;
        public override InternalAiBase InternalAi { get; protected set; }

        public override IReadOnlyList<IAbility> Abilities => ActiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities => _activeAbilities;
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities { get; } = new List<IPassiveAbility>();
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _cooldownProcessor = new CooldownProcessor(1);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.AttackDamage, _cooldownProcessor);
            
            _repairAbility = new RepairAbility(this, new RepairApplier(_selector));
            _hiveProtectionAbility = new HiveProtectionAbility(this, new HiveProtectionApplier(_selector));
            _activeAbilities.Add(_repairAbility);
            _activeAbilities.Add(_hiveProtectionAbility);

            _repairProcessor = new RepairProcessor(config.RepairValue, config.RepairCooldown);
            _resourceExtractionProcessor = new ResourceExtractionProcessor(this, config.GatheringCapacity, config.GatheringTime,
                _teamsResourcesGlobalStorage, resourceSkin);
            _orderValidator = new WorkerBeeValidator(this, config.InteractionRange, _resourceExtractionProcessor);
            
            var stateBases = new List<EntityStateBase>()
            {
                new IdleState(),
                new MoveState(this, _orderValidator),
                new BuildState(this),
                new RepairState(this, _repairProcessor),
                new ResourceExtractionState(this, _resourceExtractionProcessor),
                new StorageResourceState(this, _resourceExtractionProcessor),
                new HideInConstructionState(this, this, ReturnInPool),
                new WorkerBeeAttackState(this, _attackProcessor, _cooldownProcessor)
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);

            InternalAi = new WorkerBeeInternalAi(this, stateBases, _missionData);
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
            _repairProcessor.Reset();
            _repairAbility.Reset();
            _hiveProtectionAbility.Reset();

            _stateMachine.SetState(EntityStateID.Idle);
        }

        public override void Initialize(AffiliationEnum affiliation)
        {
            base.Initialize(affiliation);
            
            var tech = _technologyModule.GetTechnology<TechWorkerBeeResourcesExtension>(Affiliation, TechnologyId.WorkerBeeResourcesExtension);
            _resourceExtractionProcessor.SetTechnology(tech);
        }

        public HiderCellBase TakeHideCell()
            => new WorkerBeeHiderCell(this, _resourceExtractionProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out WorkerBeeHiderCell workerBeeHideCell))
            {
                _healthStorage.SetValue(workerBeeHideCell.HealthPoints.CurrentValue);
                _resourceExtractionProcessor.LoadData(workerBeeHideCell.ResourceExtracted, workerBeeHideCell.ExtractedResourceID);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}
    