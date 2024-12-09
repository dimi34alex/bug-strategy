using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.Ai.UnitAis;
using BugStrategy.EntityState;
using BugStrategy.Missions;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Butterflies
{
    public class CaterpillarLevel2 : ButterflyUnit, IHidableUnit
    {
        [SerializeField] private CaterpillarLevel2Config config;
        [SerializeField] private GameObject resourceSkin;

        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public override UnitType UnitType => UnitType.CaterpillarLevel2;
        protected override OrderValidatorBase OrderValidator => _orderValidator;

        private OrderValidatorBase _orderValidator;
        private ResourceExtractionProcessor _resourceExtractionProcessor;
        public IReadOnlyResourceExtractionProcessor ResourceExtractionProcessor => _resourceExtractionProcessor;
        public override InternalAiBase InternalAi { get; protected set; }

        public override IReadOnlyList<IAbility> Abilities => ActiveAbilities;
        public override IReadOnlyList<IActiveAbility> ActiveAbilities { get; } = new List<IActiveAbility>();
        public override IReadOnlyList<IPassiveAbility> PassiveAbilities { get; } = new List<IPassiveAbility>();
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new FloatStorage(config.HealthPoints, config.HealthPoints);
           
            _resourceExtractionProcessor = new ResourceExtractionProcessor(this, config.GatheringCapacity, config.GatheringTime,
                _teamsResourcesGlobalStorage, resourceSkin);
            _orderValidator = new CaterpillarLevel2Validator(this, config.InteractionRange, _resourceExtractionProcessor);
            
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

            InternalAi = new CaterpillarLevel2InternalAi(this, stateBases, _missionData);
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
            => new CaterpillarLevel2HiderCell(this, _resourceExtractionProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out CaterpillarLevel2HiderCell caterpillarLevel2HideCell))
            {
                _healthStorage.SetValue(caterpillarLevel2HideCell.HealthPoints.CurrentValue);
                _resourceExtractionProcessor.LoadData(caterpillarLevel2HideCell.ResourceExtracted, caterpillarLevel2HideCell.ExtractedResourceID);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}
    