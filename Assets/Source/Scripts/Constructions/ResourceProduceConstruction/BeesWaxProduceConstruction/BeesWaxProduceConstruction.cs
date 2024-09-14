using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.UnitsHideCore;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction
{
    public class BeesWaxProduceConstruction : ResourceConversionConstructionBase, IEvolveConstruction, IHiderConstruction
    {
        [SerializeField] private BeesWaxProduceConfig config;
        [SerializeField] private Transform hiderExtractPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        private UnitsHider _hider;
        
        private ResourceConversionCore _resourceConversionCore;
        private ResourceProduceConstructionState _produceConstructionState;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeWaxProduceConstruction;
        public override ResourceProduceConstructionState ProduceConstructionState => _produceConstructionState;
        public override ResourceConversionCore ResourceConversionCore => _resourceConversionCore;
        public IHider Hider => _hider;

        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new BeesWaxProduceLevelSystem(this, config, _unitFactory, hiderExtractPosition, _teamsResourcesGlobalStorage,
                _healthStorage, ref _resourceConversionCore, ref _hider);

            _updateEvent += OnUpdate;
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);

        private void OnUpdate()
        {
            if (_produceConstructionState != ResourceProduceConstructionState.Proccessing)
                return;

            if (!_resourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Completed;

            _resourceConversionCore.Tick(Time.deltaTime);
        }

        public void SetConversionPauseState(bool paused)
        {
            if (_produceConstructionState is ResourceProduceConstructionState.Completed)
                return;

            _produceConstructionState =
                paused ? ResourceProduceConstructionState.Paused : ResourceProduceConstructionState.Proccessing;
        }

        public void AddSpendableResource(int addPollen)
        {
            var pollen = _teamsResourcesGlobalStorage.GetResource(Affiliation, ResourceID.Pollen);
            IReadOnlyFloatStorage spendableResource = _resourceConversionCore.SpendableResource;

            if (pollen.CurrentValue > 0 && spendableResource.CurrentValue < spendableResource.Capacity)
            {
                addPollen = (int)Mathf.Clamp(addPollen, 0, pollen.Capacity - pollen.CurrentValue);
                addPollen = (int)Mathf.Clamp(addPollen, 0, spendableResource.Capacity - spendableResource.CurrentValue);
                _resourceConversionCore.AddSpendableResource(addPollen);
                _teamsResourcesGlobalStorage.ChangeValue(Affiliation, ResourceID.Pollen, -addPollen);
            }

            if (_resourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            IReadOnlyFloatStorage produceResource = _resourceConversionCore.ProducedResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }

        public void ExtractProduceResource()
        {
            var beesWax = _teamsResourcesGlobalStorage.GetResource(Affiliation, ResourceID.BeesWax);
            IReadOnlyFloatStorage produceResource = _resourceConversionCore.ProducedResource;

            if (produceResource.CurrentValue > 0 && beesWax.CurrentValue < beesWax.Capacity)
            {
                int extractValue = (int)produceResource.CurrentValue;
                extractValue = (int)Mathf.Clamp(extractValue, 0, (beesWax.Capacity - beesWax.CurrentValue));
                int addBeesWax = _resourceConversionCore.ExtractProducedResources(extractValue);
                _teamsResourcesGlobalStorage.ChangeValue(Affiliation, ResourceID.BeesWax, addBeesWax);
            }

            if (_resourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            IReadOnlyFloatStorage spendableResource = _resourceConversionCore.SpendableResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }

        public IReadOnlyFloatStorage TakeSpendableResourceInformation()
        {
            return _resourceConversionCore.SpendableResource;
        }

        public IReadOnlyFloatStorage TakeProduceResourceInformation()
        {
            return _resourceConversionCore.ProducedResource;
        }
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}