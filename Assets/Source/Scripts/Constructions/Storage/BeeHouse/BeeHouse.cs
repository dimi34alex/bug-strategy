using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class BeeHouse : StorageBase, IHiderConstruction
    {
        [SerializeField] private BeeHouseConfig config;
        [SerializeField] private Transform hiderExtractPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        
        private UnitsHider _hider;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeHouse;
        public IHider Hider => _hider;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeHouseLevelSystem(config, hiderExtractPosition, _unitFactory, ref resourceRepository,
                ref _healthStorage, ref _hider);
        }
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}