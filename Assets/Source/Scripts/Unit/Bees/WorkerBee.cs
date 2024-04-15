using System.Collections.Generic;
using Constructions.UnitsHideConstruction.Cells.BeesHiderCells;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnitsHideCore;
using UnityEngine;

namespace Unit.Bees
{
    public class WorkerBee : BeeUnit, IHidableUnit
    {
        [SerializeField] private BeeWorkerConfig config;
        [SerializeField] private GameObject resourceSkin;

        public override UnitType UnitType => UnitType.WorkerBee;
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private OrderValidatorBase _orderValidator;
        private ResourceExtractionProcessor _resourceExtractionProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
           
            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            _resourceExtractionProcessor = new ResourceExtractionProcessor(config.GatheringCapacity, config.GatheringTime,
                resourceRepository, resourceSkin);
            _orderValidator = new WorkerBeeValidator(this, config.InteractionRange, _resourceExtractionProcessor);
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
    