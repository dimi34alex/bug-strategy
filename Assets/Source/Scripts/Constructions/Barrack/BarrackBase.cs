using Constructions.LevelSystemCore;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using Unit.Factory;
using UnitsRecruitingSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public abstract class BarrackBase : ConstructionBase, IEvolveConstruction, IRecruitingConstruction
    {
        [SerializeField] protected Transform unitsSpawnPosition;

        [Inject] protected readonly UnitFactory _unitFactory;
        [Inject] protected readonly IResourceGlobalStorage _resourceGlobalStorage;

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
        
        public void RecruitUnit(UnitType unitType, int stackIndex) 
            => _recruiter.RecruitUnit(unitType, stackIndex);
    }
}