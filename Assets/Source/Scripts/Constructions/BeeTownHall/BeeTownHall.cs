using Constructions.LevelSystemCore;
using Unit.Factory;
using UnityEngine;
using UnitsRecruitingSystemCore;
using Zenject;

namespace Constructions
{
    public class BeeTownHall : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private BeeTownHallConfig config;
        [SerializeField] private Transform workerBeesSpawnPosition;

        [Inject] private readonly UnitFactory _unitFactory;
        
        public override ConstructionID ConstructionID => ConstructionID.Bees_Town_Hall;
        public IReadOnlyUnitsRecruiter Recruiter => _recruiter;
        public IConstructionLevelSystem LevelSystem { get; private set; }

        private UnitsRecruiter _recruiter;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            gameObject.name = "TownHall";

            var takeResourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeTownHallLevelSystem(config.Levels, workerBeesSpawnPosition, _unitFactory, 
                ref takeResourceRepository, ref _healthStorage, ref _recruiter);
        
            _updateEvent += OnUpdate;
            _onDestroy += SetLooseScreen;
        }

        private void OnUpdate()
        {
            _recruiter.Tick(Time.deltaTime);
        }
        
        public void RecruitingWorkerBee(UnitType beeID)
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
        
        private void SetLooseScreen()
        {
            UI_Controller._SetWindow("UI_Lose");
            _onDestroy -= SetLooseScreen;
        }
    }  
}
