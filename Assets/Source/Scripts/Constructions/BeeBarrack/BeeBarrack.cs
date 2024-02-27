using Constructions.LevelSystemCore;
using Unit.Factory;
using UnityEngine;
using UnitsRecruitingSystemCore;
using Zenject;

namespace Constructions
{
    public class BeeBarrack : ConstructionBase, IEvolveConstruction
    {
        public override ConstructionID ConstructionID => ConstructionID.BeeBarrack;

        [SerializeField] private BeeBarrackConfig config;
        [SerializeField] private Transform beesSpawnPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }

        private UnitsRecruiter _recruiter;
        public IReadOnlyUnitsRecruiter Recruiter => _recruiter;

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeBarrackLevelSystem(config.Levels, beesSpawnPosition, _unitFactory,
                ref resourceRepository, ref _healthStorage, ref _recruiter);

            _updateEvent += OnUpdate;
        }

        private void OnUpdate()
        {
            _recruiter.Tick(Time.deltaTime);
        }

        public void RecruitBees(UnitType beeID)
        {
            int freeStackIndex = _recruiter.FindFreeStack();

            if (freeStackIndex == -1)
            {
                UI_Controller._ErrorCall("All stacks are busy");
                return;
            }

            if (!_recruiter.CheckCosts(beeID))
            {
                UI_Controller._ErrorCall("Need more resources");
                return;
            }

            _recruiter.RecruitUnit(beeID, freeStackIndex);
        }
    }
}