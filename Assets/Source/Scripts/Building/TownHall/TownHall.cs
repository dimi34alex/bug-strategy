using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownHall : EvolvConstruction<TownHallLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;
    
    public bool AlarmOn => alarmOn;
    bool alarmOn = false;//тревога включена?
    public static event UnityAction WorkerBeeAlarmOn;//оповещение рабочих пчел о тревоге
    static Stack<GameObject> WorkerBeesInTownHall = new Stack<GameObject>();//массив пчел, которые спрятались в ратуши

    [SerializeField] [Range(0,5)] float pauseTimeOfOutBeesFromTownHallAfterAlarm = 1;//пауза между выходами пчел из здания после выключения тревоги
    [SerializeField] Transform workerBeesSpawnPosition;//координаты флага, на котором спавняться рабочие пчелы
    private BeesRecruiting _recruiting;
    public int RecruitingSize => CurrentLevel.RecruitingSize;

    public Affiliation team;

    protected override void OnAwake()
    {
        base.OnAwake();
        gameObject.name = "TownHall";
        
        _recruiting = new BeesRecruiting(CurrentLevel.RecruitingSize, workerBeesSpawnPosition, CurrentLevel.BeesRecruitingData);
        
        levelSystem = new TownHallLevelSystem(levelSystem, HealPoints, _recruiting);
        
        _updateEvent += OnUpdate;
        _onDestroy += OnDestroy;
    }

    public void AddResource(ResourceID resourceID,float value)
    {
        ResourceGlobalStorage.ChangeValue(resourceID, value);
    }

    void OnUpdate()
    {
        _recruiting.Tick(Time.deltaTime, team.affiliation);
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
        
        _recruiting.RecruitBees(beeID);
    }
    
    public List<BeeRecruitingInformation> GetRecruitingInformation()
    {
        return _recruiting.GetRecruitingInformation();
    }

    private void OnDestroy()
    {
        UI_Controller._SetWindow("UI_Lose");
        _onDestroy -= OnDestroy;
    }
}