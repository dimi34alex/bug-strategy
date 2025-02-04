using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.UnitsHideCore;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.BeeHouse
{
    public class BeeHouse : StorageBase, IHiderConstruction
    {
        [SerializeField] private BeeHouseConfig config;
        [SerializeField] private Transform hiderExtractPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeHouse;
        protected override ConstructionConfigBase ConfigBase => config;
        public IHider Hider => _hider;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new BeeHouseLevelSystem(this, config, hiderExtractPosition, _unitFactory, _teamsResourcesGlobalStorage,
                _healthStorage, ref _hider);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);

        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}