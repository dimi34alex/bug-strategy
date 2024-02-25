using Constructions.LevelSystemCore;
using UnityEngine;
using UnitsRecruitingSystem;

namespace Constructions
{
    public class BeeBarrack : EvolvConstruction
    {
        public override ConstructionID ConstructionID => ConstructionID.BeeBarrack;

        [SerializeField] private BeeBarrackConfig config;
        [SerializeField] private Transform beesSpawnPosition;

        private UnitsRecruiter<BeesRecruitingID> _recruiter;
        public IReadOnlyUnitsRecruiter<BeesRecruitingID> Recruiter => _recruiter;

        protected override void OnAwake()
        {
            base.OnAwake();

            levelSystem = new BeeBarrackLevelSystem(config.Levels, beesSpawnPosition, ref _healthStorage, ref _recruiter);

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