using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Unit;
using BugStrategy.Unit.Pricing;
using BugStrategy.Unit.RecruitingSystem;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntTownHall
{
    public class AntTownHall : TownHallBase
    {
        [SerializeField] private AntTownHallConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntTownHall;
        protected override ConstructionConfigBase ConfigBase => config;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _recruiter = new UnitsRecruiter(this, 0, workerBeesSpawnPosition, _unitFactory, _teamsResourcesGlobalStorage, _unitsCostsProvider);
            LevelSystem = new AntTownHallLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage, _healthStorage, _recruiter);
            
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
        //TODO: temporary code. Remove this, when ants ui will be create
        [ContextMenu("RecruitAntStandard")]
        private void RecruitAntStandard() => RecruitUnit(UnitType.AntStandard);
        [ContextMenu("RecruitAntBig")]
        private void RecruitAntBig() => RecruitUnit(UnitType.AntBig);
        [ContextMenu("RecruitAntFly")]
        private void RecruitAntFly() => RecruitUnit(UnitType.AntFlying);
    }
}