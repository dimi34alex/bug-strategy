using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class BeesWaxProduceConstruction : ResourceConversionConstructionBase, IEvolveConstruction, IHiderConstruction
    {
        [SerializeField] private BeesWaxProduceConfig config;
        [SerializeField] private Transform hiderExtractPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;
        
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

            LevelSystem = new BeesWaxProduceLevelSystem(this, config, _unitFactory, hiderExtractPosition, _resourceGlobalStorage,
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
            ResourceBase pollen = _resourceGlobalStorage.GetResource(Affiliation, ResourceID.Pollen);
            IReadOnlyResourceStorage spendableResource = _resourceConversionCore.SpendableResource;

            if (pollen.CurrentValue > 0 && spendableResource.CurrentValue < spendableResource.Capacity)
            {
                addPollen = (int)Mathf.Clamp(addPollen, 0, pollen.Capacity - pollen.CurrentValue);
                addPollen = (int)Mathf.Clamp(addPollen, 0, spendableResource.Capacity - spendableResource.CurrentValue);
                _resourceConversionCore.AddSpendableResource(addPollen);
                _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Pollen, -addPollen);
            }

            if (_resourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            IReadOnlyResourceStorage produceResource = _resourceConversionCore.ProducedResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }

        public void ExtractProduceResource()
        {
            ResourceBase beesWax = _resourceGlobalStorage.GetResource(Affiliation, ResourceID.Bees_Wax);
            IReadOnlyResourceStorage produceResource = _resourceConversionCore.ProducedResource;

            if (produceResource.CurrentValue > 0 && beesWax.CurrentValue < beesWax.Capacity)
            {
                int extractValue = (int)produceResource.CurrentValue;
                extractValue = (int)Mathf.Clamp(extractValue, 0, (beesWax.Capacity - beesWax.CurrentValue));
                int addBeesWax = _resourceConversionCore.ExtractProducedResources(extractValue);
                _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Bees_Wax, addBeesWax);
            }

            if (_resourceConversionCore.ConversionIsAvailable)
                _produceConstructionState = ResourceProduceConstructionState.Proccessing;

            IReadOnlyResourceStorage spendableResource = _resourceConversionCore.SpendableResource;
            if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
                SetConversionPauseState(false);
            else
                SetConversionPauseState(true);
        }

        public IReadOnlyResourceStorage TakeSpendableResourceInformation()
        {
            return _resourceConversionCore.SpendableResource;
        }

        public IReadOnlyResourceStorage TakeProduceResourceInformation()
        {
            return _resourceConversionCore.ProducedResource;
        }
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}