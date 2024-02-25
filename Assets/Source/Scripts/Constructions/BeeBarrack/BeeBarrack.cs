using Constructions.LevelSystemCore;
using UnityEngine;
using UnitsRecruitingSystem;

namespace Constructions
{
    public class BeeBarrack : ConstructionBase, IEvolveConstruction
    {
        public override ConstructionID ConstructionID => ConstructionID.BeeBarrack;

        [SerializeField] private BeeBarrackConfig config;
        [SerializeField] private Transform beesSpawnPosition;

        public IConstructionLevelSystem LevelSystem { get; private set; }

        private UnitsRecruiter<BeesRecruitingID> _recruiter;
        public IReadOnlyUnitsRecruiter<BeesRecruitingID> Recruiter => _recruiter;

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeBarrackLevelSystem(config.Levels, beesSpawnPosition, ref resourceRepository, ref _healthStorage, ref _recruiter);

            _updateEvent += OnUpdate;
        }

        private void OnUpdate()
        {
            _recruiter.Tick(Time.deltaTime);
        }

        public void RecruitBees(BeesRecruitingID beeID)
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