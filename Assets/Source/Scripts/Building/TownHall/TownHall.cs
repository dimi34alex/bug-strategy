using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownHall : ConstructionBase
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    #region Main
    protected UI_Controller UI;
    public float MaxHealPoints => currentLevel.MaxHealPoints;
    public float HealPoints => healPoints;
    protected float healPoints = 0;
    #endregion

    #region Resources
    public float MaxPollen => currentLevel.PollenCapacity;
    public float MaxWax => currentLevel.BeesWaxCapacity;
    #endregion

    #region level-ups
    [SerializeField] List<TownHallLevel> levels;//массив уровней здания
    TownHallLevel currentLevel;//текущий уровень
    int currentLevelNum = 1;
    
    //цена в кол-ве ресурсов для повышения до следующего лвл-а ратуши, согласно текущему лвл-у
    public float PollenPrice => currentLevel.PollenLevelUpPrice;
    public float WaxPrice => currentLevel.BeesWaxLevelUpPrice;
    #endregion

    #region Workers Bees
    public bool AlarmOn => alarmOn;
    bool alarmOn = false;//тревога включена?
    public static UnityEvent WorkerBeeAlarmOn = new UnityEvent();//оповещение рабочих пчел о тревоге
    static Stack<GameObject> WorkerBeesInTownHall = new Stack<GameObject>();//массив пчел, которые спрятались в ратуши

    [SerializeField] [Range(0,5)] float pauseTimeOfOutBeesFromTownHallAfterAlarm = 1;//пауза между выходами пчел из здания после выключения тревоги
    [SerializeField] Transform workerBeesSpawnPosition;//координаты флага, на котором спавняться рабочие пчелы
    BeesRecruiting recruiting;
    public int RecruitingSize => currentLevel.RecruitingSize;
    #endregion

    protected override void OnAwake()
    {
        base.OnAwake();
        gameObject.name = "TownHall";
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();

        currentLevel = levels[0];
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen,currentLevel.PollenCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax,currentLevel.BeesWaxCapacity);
        
        recruiting = new BeesRecruiting(currentLevel.RecruitingSize, workerBeesSpawnPosition, currentLevel.BeesRecruitingData);

        _updateEvent += OnUpdate;
    }

    #region Resource methods
    public void _AddResurce(ResourceID resourceID,float value)
    {
        ResourceGlobalStorage.ChangeValue(resourceID, value);
    }
    #endregion

    #region  Woreker Bees Methods
    void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime);
    }
    public static void _HideMe(GameObject workerBee)
    {
        WorkerBeesInTownHall.Push(workerBee);
        workerBee.SetActive(false);
    }
    public void _WorkerBeeAlarmer()
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
    public string _RecruitingWorkerBee(BeesRecruitingID beeID)
    {
        return recruiting.RecruitBees(beeID);
    }
    public BeeRecruitingInformation _GetBeeRecruitingInformation(int n)
    {
        return recruiting.GetBeeRecruitingInformation(n);
    }
    #endregion

    #region BuildingsMainMethods
    public void _CallBuildingMenu(string windowName)//вызов меню здания
    {
        UI._SetBuilding(gameObject, windowName);
    }
    public void _NextBuildingLevel()//повышение уровня здания, вызывется через UI/UX
    {
        Debug.LogError("lvlupTownHall");

        if (currentLevelNum == levels.Count)
        {
            Debug.Log("max Town Hall level");
            return;
        }
        
        if (ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue >= currentLevel.PollenLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue >= currentLevel.BeesWaxLevelUpPrice)
        {
            float pollenPrevCapacity = currentLevel.PollenCapacity;
            float beesWaxPrevCapacity = currentLevel.BeesWaxCapacity;
            
            currentLevel = levels[currentLevelNum++];
            
            ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, currentLevel.PollenCapacity - pollenPrevCapacity);
            ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, currentLevel.BeesWaxCapacity - beesWaxPrevCapacity);

            recruiting.AddStacks(currentLevel.RecruitingSize);
            recruiting.SetNewBeesDatas(currentLevel.BeesRecruitingData);

            Debug.Log("Building LVL = " + currentLevelNum);
        }
        else
            Debug.Log("Need more resources");
    }
    public void _GetDamage(float damage)//получение урона
    {
        healPoints -= damage;
        if (healPoints <= 0)
        {
            Destroy(gameObject);
            UI._SetWindow("UI_Lose");
        }
    }
    public void _Repair(float addHP)
    {
        healPoints += addHP;
        if (healPoints > MaxHealPoints)
            healPoints = MaxHealPoints;
    }
    #endregion
}