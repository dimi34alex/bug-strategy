using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.TechnologiesSystem;
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
        [Inject] private readonly TechnologyModule _technologyModule;

        private UnitsHider _hider;
        private ResourceConversionCore _resourceConversionCore;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeWaxProduceConstruction;
        public override ResourceConversionCore ResourceConversionCore => _resourceConversionCore;
        protected override ConstructionConfigBase ConfigBase => config;
        public IHider Hider => _hider;

        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new BeesWaxProduceLevelSystem(this, _technologyModule, config, _unitFactory, hiderExtractPosition, TeamsResourcesGlobalStorage,
                _healthStorage, ref _resourceConversionCore, ref _hider);

            Initialized += InitLevelSystem;
            
            OnDeactivation += ReleaseUnitsHider;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);

        private void ReleaseUnitsHider(ITarget _) 
            => _hider.ExtractAll();
    }
}