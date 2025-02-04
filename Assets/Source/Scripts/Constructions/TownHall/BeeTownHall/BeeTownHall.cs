using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.UnitsHideCore;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.BeeTownHall
{
    public class BeeTownHall : TownHallBase, IHiderConstruction
    {
        [SerializeField] private BeeTownHallConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeTownHall;
        protected override ConstructionConfigBase ConfigBase => config;
        public IHider Hider => _hider;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            gameObject.name = "TownHall";

            _recruiter = new UnitsRecruiter(this, 0, workerBeesSpawnPosition, _unitFactory, _teamsResourcesGlobalStorage);
            _hider = new UnitsHider(this, 0, _unitFactory, workerBeesSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeTownHallLevelSystem(this, config, workerBeesSpawnPosition, _unitFactory, 
                _teamsResourcesGlobalStorage, _healthStorage, ref _recruiter, ref _hider);
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
