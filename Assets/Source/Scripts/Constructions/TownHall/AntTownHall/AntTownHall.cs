using Construction.TownHalls;
using Constructions.LevelSystemCore;
using UnitsRecruitingSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class AntTownHall : TownHallBase
    {
        [SerializeField] private AntTownHallConfig config;

        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntTownHall;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _recruiter = new UnitsRecruiter(this, 0, workerBeesSpawnPosition, _unitFactory, _resourceGlobalStorage);
            LevelSystem = new AntTownHallLevelSystem(this, config, _resourceGlobalStorage, _healthStorage, _recruiter);
            
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