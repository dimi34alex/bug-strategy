using System.Collections.Generic;
using Unit.Bees.Configs;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using Unit.States;
using UnityEngine;

namespace Unit.Bees
{
    public class WorkerBee : BeeUnit
    {
        [SerializeField] private BeeWorkerConfig config;
        [SerializeField] private GameObject resourceSkin;

        public override UnitType UnitType => UnitType.WorkerBee;
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private WorkerOrderValidator _orderValidator;
        private ResourceExtractionProcessor _resourceExtractionProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
           
            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            _resourceExtractionProcessor = new ResourceExtractionProcessor(config.GatheringCapacity, config.GatheringTime,
                resourceRepository, resourceSkin);
            _orderValidator = new WorkerOrderValidator(this, config.InteractionRange, _resourceExtractionProcessor);
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
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);
        }
    }
}
    