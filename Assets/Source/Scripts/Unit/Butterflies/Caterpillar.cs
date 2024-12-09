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
    public class Caterpillar : ButterflyUnit, IHidableUnit
    {
        [SerializeField] private CaterpillarConfig config;
        [SerializeField] private GameObject resourceSkin;

        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public override UnitType UnitType => UnitType.Caterpillar;
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
            _orderValidator = new CaterpillarValidator(this, config.InteractionRange, _resourceExtractionProcessor);
            
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

            InternalAi = new CaterpillarInternalAi(this, stateBases, _missionData);
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
            => new CaterpillarHiderCell(this, _resourceExtractionProcessor);

        public void LoadHideCell(HiderCellBase hiderCell)
        {
            if (hiderCell.TryCast(out CaterpillarHiderCell caterpillarHideCell))
            {
                _healthStorage.SetValue(caterpillarHideCell.HealthPoints.CurrentValue);
                _resourceExtractionProcessor.LoadData(caterpillarHideCell.ResourceExtracted, caterpillarHideCell.ExtractedResourceID);
            }
            else
            {
                Debug.LogError($"Invalid hider cell");
            }
        }
    }
}
    