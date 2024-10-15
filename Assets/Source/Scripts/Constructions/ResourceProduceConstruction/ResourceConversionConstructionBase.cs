using BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ResourceProduceConstruction
{
    public abstract class ResourceConversionConstructionBase : ResourceProduceConstructionBase
    {
        public abstract ResourceConversionCore ResourceConversionCore { get; }
        public override ResourceProduceCoreBase ResourceProduceCoreBase => ResourceConversionCore;
        public override ResourceProduceConstructionState ProduceConstructionState => _produceConstructionState;

        public IReadOnlyFloatStorage SpendableResource => ResourceConversionCore.SpendableResource;
        public IReadOnlyFloatStorage ProducedResource => ResourceConversionCore.ProducedResource;
        
        [Inject] protected readonly ITeamsResourcesGlobalStorage TeamsResourcesGlobalStorage;
        private ResourceProduceConstructionState _produceConstructionState;

        protected override void OnAwake()
        {
            base.OnAwake();

            _updateEvent += OnUpdate;
        }
        
        private void OnUpdate()
        {
            if (_produceConstructionState != ResourceProduceConstructionState.Proccessing)
                return;

            if (!ResourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Completed;

            ResourceConversionCore.Tick(Time.deltaTime);
        }
        
        public void AddSpendableResource(int value)
        {
            var resourceStorage = TeamsResourcesGlobalStorage.GetResource(Affiliation, ResourceConversionCore.SpendableResourceID);
            var spendableResource = SpendableResource;

            if (resourceStorage.CurrentValue > 0 && spendableResource.CurrentValue < spendableResource.Capacity)
            {
                value = (int)Mathf.Clamp(value, 0, resourceStorage.CurrentValue);
                value = (int)Mathf.Clamp(value, 0, spendableResource.Capacity - spendableResource.CurrentValue);
                ResourceConversionCore.AddSpendableResource(value);
                TeamsResourcesGlobalStorage.ChangeValue(Affiliation, ResourceConversionCore.SpendableResourceID, -value);
            }

            if (ResourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            var produceResource = ProducedResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }
        
        public void ExtractProduceResource()
        {
            var resourceStorage = TeamsResourcesGlobalStorage.GetResource(Affiliation, ResourceConversionCore.TargetResourceID);
            var produceResource = ProducedResource;

            if (produceResource.CurrentValue > 0 && resourceStorage.CurrentValue < resourceStorage.Capacity)
            {
                var extractValue = (int)Mathf.Clamp(produceResource.CurrentValue, 0, resourceStorage.Capacity - resourceStorage.CurrentValue);
                extractValue = ResourceConversionCore.ExtractProducedResources(extractValue);
                TeamsResourcesGlobalStorage.ChangeValue(Affiliation, ResourceConversionCore.TargetResourceID, extractValue);
            }

            if (ResourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            var spendableResource = SpendableResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }
        
        public void SetConversionPauseState(bool paused)
        {
            if (_produceConstructionState is ResourceProduceConstructionState.Completed)
                return;

            _produceConstructionState =
                paused ? ResourceProduceConstructionState.Paused : ResourceProduceConstructionState.Proccessing;
        }
    }
}
