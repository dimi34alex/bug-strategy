using System.Collections;
using System.Collections.Generic;
using ConstructionLevelSystem;
using UnityEngine;
using UnityEngine.Events;
using UnitsRecruitingSystem;

public class TownHall : EvolvConstruction<TownHallLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;
    
    public bool AlarmOn => alarmOn;
    bool alarmOn = false;//тревога включена?
    public static event UnityAction WorkerBeeAlarmOn;//оповещение рабочих пчел о тревоге
    static Stack<GameObject> WorkerBeesInTownHall = new Stack<GameObject>();//массив пчел, которые спрятались в ратуши

    [SerializeField] [Range(0,5)] float pauseTimeOfOutBeesFromTownHallAfterAlarm = 1;//пауза между выходами пчел из здания после выключения тревоги
    [SerializeField] Transform workerBeesSpawnPosition;//координаты флага, на котором спавняться рабочие пчелы

    private UnitsRecruiter<BeesRecruitingID> _recruiter;
    public IReadOnlyUnitsRecruiter<BeesRecruitingID> Recruiter => _recruiter;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        gameObject.name = "TownHall";

        levelSystem = new TownHallLevelSystem(levelSystem, workerBeesSpawnPosition, ref _healthStorage, ref _recruiter);
        
        _updateEvent += OnUpdate;
        _onDestroy += OnDestroy;
    }

    public void AddResource(ResourceID resourceID,float value)
    {
        ResourceGlobalStorage.ChangeValue(resourceID, value);
    }

    void OnUpdate()
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
        alarmOn = !alarmOn;
        if (alarmOn)
            WorkerBeeAlarmOn?.Invoke();
        else
            StartCoroutine(OutBeesFromTownHall());
    }
    
    IEnumerator OutBeesFromTownHall()
    {
        GameObject bee;
        while (WorkerBeesInTownHall.Count > 0 && !alarmOn)
        {
            bee = WorkerBeesInTownHall.Pop();
            bee.SetActive(true);
            yield return new WaitForSeconds(pauseTimeOfOutBeesFromTownHallAfterAlarm);
        }
    }
    
    public void RecruitingWorkerBee(BeesRecruitingID beeID)
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
    
    public List<IReadOnlyUnitRecruitingStack<BeesRecruitingID>> GetRecruitingInformation()
    {
        return _recruiter.GetRecruitingInformation();
    }

    private void OnDestroy()
    {
        UI_Controller._SetWindow("UI_Lose");
        _onDestroy -= OnDestroy;
    }
}