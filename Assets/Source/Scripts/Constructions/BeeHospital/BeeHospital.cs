using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Constructions.BeeHospital
{
    public class BeeHospital : ConstructionBase, IEvolveConstruction, IHiderConstruction
    {
        [SerializeField] private BeeHospitalConfig config;
        [SerializeField] private Transform hiderExtractionPosition;

        [Inject] private UnitFactory _unitFactory;
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeHospital;
        public IHider Hider => _hider;

        public IConstructionLevelSystem LevelSystem => _levelSystem;
        private BeeHospitalLevelSystem _levelSystem;
        private HealProcessor _healProcessor;
        private UnitsHider _hider;

        protected override void OnAwake()
        {
            base.OnAwake();

            _hider = new UnitsHider(0, _unitFactory, hiderExtractionPosition, config.HiderAccess);
            _healProcessor = new HealProcessor(_hider, 0);
            
            _levelSystem = new BeeHospitalLevelSystem(this, config, _resourceGlobalStorage, _healthStorage, 
                _hider, _healProcessor);

            Initialized += InitializeLevelSystem;
            _updateEvent += UpdateHealProcessor;
        }

        private void InitializeLevelSystem()
            => _levelSystem.Init(0);

        private void UpdateHealProcessor()
            => _healProcessor.HandleUpdate(Time.deltaTime);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}