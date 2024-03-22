using System.Collections.Generic;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;
using Unit.States;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class Murmur : BeeUnit
    {
        [SerializeField] private MurmurConfig config;
        [SerializeField] private GameObject resourceSkin;
        
        public override UnitType UnitType => UnitType.Murmur;
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private ResourceExtractionProcessor _resourceExtractionProcessor;
        private MurmurOrderValidator _orderValidator;
        private AttackProcessorBase _attackProcessor;
        private CooldownProcessor _cooldownProcessor;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            
            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            _resourceExtractionProcessor = new ResourceExtractionProcessor(config.GatheringCapacity, config.GatheringTime,
                resourceRepository, resourceSkin);
            _cooldownProcessor = new CooldownProcessor(config.AttackCooldown);
            _attackProcessor = new MeleeAttackProcessor(this, config.AttackRange, config.AttackDamage, _cooldownProcessor);
            _orderValidator = new MurmurOrderValidator(this, config.InteractionRange, _attackProcessor, 
                _cooldownProcessor, _resourceExtractionProcessor);
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
            
            var stateBases = new List<EntityStateBase>()
            {
                new IdleState(),
                new MoveState(this, _orderValidator),
                new ResourceExtractionState(this, _resourceExtractionProcessor),
                new StorageResourceState(this, _resourceExtractionProcessor),
                new AttackState(this, _attackProcessor, _cooldownProcessor)
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);
        }
    }
}