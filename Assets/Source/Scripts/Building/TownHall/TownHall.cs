using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownHall : EvolvConstruction<TownHallLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    protected UI_Controller UI;

    public float PollenPrice => CurrentLevel.PollenLevelUpPrice;
    public float WaxPrice => CurrentLevel.BeesWaxLevelUpPrice;
    public float HousingPrice => CurrentLevel.HousingLevelUpPrice;

    public bool AlarmOn => alarmOn;
    bool alarmOn = false;//тревога включена?
    public static UnityEvent WorkerBeeAlarmOn = new UnityEvent();//оповещение рабочих пчел о тревоге
    static Stack<GameObject> WorkerBeesInTownHall = new Stack<GameObject>();//массив пчел, которые спрятались в ратуши

    [SerializeField] [Range(0,5)] float pauseTimeOfOutBeesFromTownHallAfterAlarm = 1;//пауза между выходами пчел из здания после выключения тревоги
    [SerializeField] Transform workerBeesSpawnPosition;//координаты флага, на котором спавняться рабочие пчелы
    BeesRecruiting recruiting;
    public int RecruitingSize => CurrentLevel.RecruitingSize;

    protected override void OnAwake()
    {
        base.OnAwake();
        gameObject.name = "TownHall";
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
        
        recruiting = new BeesRecruiting(CurrentLevel.RecruitingSize, workerBeesSpawnPosition, CurrentLevel.BeesRecruitingData);
        
        levelSystem = new TownHallLevelSystem(levelSystem, HealPoints, recruiting);

        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen,CurrentLevel.PollenCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax,CurrentLevel.BeesWaxCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing,CurrentLevel.HousingCapacity);
        
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity);
        
        _updateEvent += OnUpdate;
        _onDestroy += OnDestroy;
    }

    public void AddResource(ResourceID resourceID,float value)
    {
        ResourceGlobalStorage.ChangeValue(resourceID, value);
    }

    void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime);
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
            StartCoroutine("OutBeesFromTownHall");
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
    public string RecruitingWorkerBee(BeesRecruitingID beeID)
    {
        return recruiting.RecruitBees(beeID);
    }
    public BeeRecruitingInformation _GetBeeRecruitingInformation(int n)
    {
        return recruiting.GetBeeRecruitingInformation(n);
    }

    private void OnDestroy()
    {
        UI_Controller._SetWindow("UI_Lose");
        _onDestroy -= OnDestroy;
    }
}