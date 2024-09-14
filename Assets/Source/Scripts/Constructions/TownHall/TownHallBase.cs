using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.UI;
using BugStrategy.Unit;
using BugStrategy.Unit.Factory;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions
{
    public abstract class TownHallBase : ConstructionBase, IEvolveConstruction, IRecruitingConstruction
    {
        [SerializeField] protected Transform workerBeesSpawnPosition;
       
        [Inject] protected readonly UnitFactory _unitFactory;

        public abstract IConstructionLevelSystem LevelSystem { get; protected set; }
        public IReadOnlyUnitsRecruiter Recruiter => _recruiter;

        protected UnitsRecruiter _recruiter;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            _updateEvent += OnUpdate;
        }

        private void OnUpdate()
        {
            _recruiter.Tick(Time.deltaTime);
        }
        
        public void RecruitUnit(UnitType unitID)
        {
            int freeStackIndex = _recruiter.FindFreeStack();

            if (freeStackIndex == -1)
            {
                UIController.ErrorCall("All stacks are busy");
                return;
            }

            if (!_recruiter.CheckCosts(unitID))
            {
                UIController.ErrorCall("Need more resources");
                return;
            }
        
            _recruiter.RecruitUnit(unitID, freeStackIndex);
        }
        
        public void CancelRecruit(int stackIndex)
            => _recruiter.CancelRecruit(stackIndex);
    }
}