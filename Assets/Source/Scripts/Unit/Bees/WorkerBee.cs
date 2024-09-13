using System.Collections.Generic;
using Constructions.UnitsHideConstruction.Cells.BeesHiderCells;
using Source.Scripts.Ai.InternalAis;
using Source.Scripts.Ai.UnitAis;
using Source.Scripts.Missions;
using Source.Scripts.Unit.AbilitiesCore;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class WorkerBee : BeeUnit, IHidableUnit
    {
        [SerializeField] private BeeWorkerConfig config;
        [SerializeField] private GameObject resourceSkin;

        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;
        
        public override UnitType UnitType => UnitType.WorkerBee;
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private OrderValidatorBase _orderValidator;
        private ResourceExtractionProcessor _resourceExtractionProcessor;
        public IReadOnlyResourceExtractionProcessor ResourceExtractionProcessor => _resourceExtractionProcessor;
        public override InternalAiBase InternalAi { get; protected set; }
        
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities { get; } = new List<IPassiveAbility>();
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
           
            _resourceExtractionProcessor = new ResourceExtractionProcessor(this, config.GatheringCapacity, config.GatheringTime,
                _resourceGlobalStorage, resourceSkin);
            _orderValidator = new WorkerBeeValidator(this, config.InteractionRange, _resourceExtractionProcessor);
            
            var stateBases = new List<EntityStateBase>()
            {
                new IdleState(),
                new MoveState(this, _orderValidator),
                new BuildState(this),
                new ResourceExtractionState(this, _resourceExtractionProcessor),
                new StorageResourceState(this, _resourceExtractionProcessor),
                new HideInConstructionState(this, this, ReturnInPool)
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);

            InternalAi = new WorkerBeeInternalAi(this, stateBases, _missionData);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            _resourceExtractionProcessor.HandleUpdate(time);
        }

        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _resourceExtractionProcessor.Reset();
            InternalAi.Reset();

            _stateMachine.SetState(EntityStateID.Idle);
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
    