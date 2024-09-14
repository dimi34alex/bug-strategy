using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;
using BugStrategy.Unit.Factory;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions
{
    public abstract class BarrackBase : ConstructionBase, IEvolveConstruction, IRecruitingConstruction
    {
        [SerializeField] protected Transform unitsSpawnPosition;

        [Inject] protected readonly UnitFactory _unitFactory;
        [Inject] protected readonly ITeamsResourcesGlobalStorage TeamsResourcesGlobalStorage;

        public IReadOnlyUnitsRecruiter Recruiter => _recruiter;

        public IConstructionLevelSystem LevelSystem { get; protected set; }

        protected UnitsRecruiter _recruiter;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            
            _updateEvent += RecruiterTick;
        }

        private void RecruiterTick() 
            => _recruiter.Tick(Time.deltaTime);

        public void RecruitUnit(UnitType unitType) 
            => _recruiter.RecruitUnit(unitType);

        public void CancelRecruit(int stackIndex)
            => _recruiter.CancelRecruit(stackIndex);

        public void RecruitUnit(UnitType unitType, int stackIndex) 
            => _recruiter.RecruitUnit(unitType, stackIndex);
    }
}