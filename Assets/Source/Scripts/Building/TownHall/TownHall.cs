using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TownHall : ConstructionBase, IDamagable
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    #region Main
    protected UI_Controller UI;
    public float MaxHealPoints => healPoints.Capacity;
    public float CurrentHealPoints => healPoints.CurrentValue;
    private ResourceStorage healPoints;
    #endregion

    #region Resources
    public float MaxPollen => currentLevel.PollenCapacity;
    public float MaxWax => currentLevel.BeesWaxCapacity;
    public float MaxHousing => currentLevel.BeesWaxCapacity;
    #endregion

    #region Level-ups
    [SerializeField] List<TownHallLevel> levels;
    TownHallLevel currentLevel;
    int currentLevelNum = 1;
    
    public float PollenPrice => currentLevel.PollenLevelUpPrice;
    public float WaxPrice => currentLevel.BeesWaxLevelUpPrice;
    public float HousingPrice => currentLevel.HousingLevelUpPrice;
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
        
        healPoints = new ResourceStorage(currentLevel.MaxHealPoints,currentLevel.MaxHealPoints);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen,currentLevel.PollenCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax,currentLevel.BeesWaxCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing,currentLevel.HousingCapacity);
        
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,currentLevel.HousingCapacity);
        
        recruiting = new BeesRecruiting(currentLevel.RecruitingSize, workerBeesSpawnPosition, currentLevel.BeesRecruitingData);

        _updateEvent += OnUpdate;
    }

    #region Resource methods
    public void AddResurce(ResourceID resourceID,float value)
    {
        ResourceGlobalStorage.ChangeValue(resourceID, value);
    }
    #endregion

    #region  Woreker Bees Methods
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
    #endregion

    #region BuildingsMainMethods
    public void CallBuildingMenu(string windowName)//вызов меню здания
    {
        UI._SetBuilding(gameObject, windowName);
    }
    public void NextBuildingLevel()//повышение уровня здания, вызывется через UI/UX
    {
        if (currentLevelNum == levels.Count)
        {
            Debug.Log("max Town Hall level");
            return;
        }
        
        if (ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue >= currentLevel.PollenLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue >= currentLevel.BeesWaxLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue >= currentLevel.HousingLevelUpPrice)
        {
            ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -currentLevel.PollenLevelUpPrice);
            ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, -currentLevel.BeesWaxLevelUpPrice);
            ResourceGlobalStorage.ChangeValue(ResourceID.Housing, -currentLevel.HousingLevelUpPrice);
            
            float pollenPrevCapacity = currentLevel.PollenCapacity;
            float beesWaxPrevCapacity = currentLevel.BeesWaxCapacity;
            float housingPrevCapacity = currentLevel.HousingCapacity;
            
            currentLevel = levels[currentLevelNum++];
            
            ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, currentLevel.PollenCapacity - pollenPrevCapacity);
            ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, currentLevel.BeesWaxCapacity - beesWaxPrevCapacity);
            ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, currentLevel.HousingCapacity - housingPrevCapacity);
            
            ResourceGlobalStorage.ChangeValue(ResourceID.Housing,currentLevel.HousingCapacity - housingPrevCapacity);

            recruiting.AddStacks(currentLevel.RecruitingSize);
            recruiting.SetNewBeesDatas(currentLevel.BeesRecruitingData);

            if (healPoints.CurrentValue == healPoints.Capacity)
            {
                healPoints.SetCapacity(currentLevel.MaxHealPoints);
                healPoints.SetValue(currentLevel.MaxHealPoints);
            }
            else
                healPoints.SetCapacity(currentLevel.MaxHealPoints);
            
            Debug.Log("Building LVL = " + currentLevelNum);
        }
        else
            Debug.Log("Need more resources");
    }
    
    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        healPoints.ChangeValue(-damageApplicator.Damage);
        if (CurrentHealPoints <= 0)
        {
            Destroy(gameObject);
            UI._SetWindow("UI_Lose");
        }
    }
    public void Repair(float addHP)
    {
        healPoints.ChangeValue(addHP);
    }
    #endregion
}