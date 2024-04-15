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
        
        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeHospital;
        public IHider Hider => _hider;

        public IConstructionLevelSystem LevelSystem { get; private set; }
        private UnitsHider _hider;
        private HealProcessor _healProcessor;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeHospitalLevelSystem(config, hiderExtractionPosition, _unitFactory,
                ref resourceRepository, ref _healthStorage, ref _hider, ref _healProcessor);

            _updateEvent += UpdateHealProcessor;
        }

        private void UpdateHealProcessor()
            => _healProcessor.HandleUpdate(Time.deltaTime);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}