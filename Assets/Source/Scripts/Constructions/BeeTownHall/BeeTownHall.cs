using System.Collections;
using System.Collections.Generic;
using Constructions.LevelSystemCore;
using Unit.Factory;
using UnityEngine;
using UnityEngine.Events;
using UnitsRecruitingSystemCore;
using Zenject;

namespace Constructions
{
    public class BeeTownHall : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private BeeTownHallConfig config;
        [SerializeField] [Range(0,5)] private float pauseTimeOfOutBeesFromTownHallAfterAlarm = 1;
        [SerializeField] private Transform workerBeesSpawnPosition;

        [Inject] private readonly UnitFactory _unitFactory;

        public bool AlarmIsOn { get; private set; }
    
        public override ConstructionID ConstructionID => ConstructionID.Bees_Town_Hall;
        public IReadOnlyUnitsRecruiter Recruiter => _recruiter;
        public IConstructionLevelSystem LevelSystem { get; private set; }

        static Stack<GameObject> WorkerBeesInTownHall = new Stack<GameObject>();//массив пчел, которые спрятались в ратуши
        private UnitsRecruiter _recruiter;
    
        public static event UnityAction WorkerBeeAlarmOn;//оповещение рабочих пчел о тревоге

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
    
        public static void HideMe(GameObject workerBee)
        {
            WorkerBeesInTownHall.Push(workerBee);
            workerBee.SetActive(false);
        }
    
        public void WorkerBeeAlarmer()
        {
            AlarmIsOn = !AlarmIsOn;
        if (AlarmIsOn)
            WorkerBeeAlarmOn?.Invoke();
        else
            StartCoroutine(OutBeesFromTownHall());
        }
    
        IEnumerator OutBeesFromTownHall()
        {
            GameObject bee;
            while (WorkerBeesInTownHall.Count > 0 && !AlarmIsOn)
            {
                bee = WorkerBeesInTownHall.Pop();
                bee.SetActive(true);
                yield return new WaitForSeconds(pauseTimeOfOutBeesFromTownHallAfterAlarm);
            }
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
