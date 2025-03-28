using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Unit.RecruitingSystem;
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

            _recruiter = new UnitsRecruiter(this, 0, workerBeesSpawnPosition, _unitFactory, _teamsResourcesGlobalStorage, _unitsCostsProvider);
            _hider = new UnitsHider(this, 0, _unitFactory, workerBeesSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeTownHallLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage, 
                _healthStorage, ref _recruiter, ref _hider);
            Initialized += InitLevelSystem;
            
            OnDeactivation += ReleaseUnitsHider;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);

        private void ReleaseUnitsHider(ITarget _) 
            => _hider.ExtractAll();
    }  
}
