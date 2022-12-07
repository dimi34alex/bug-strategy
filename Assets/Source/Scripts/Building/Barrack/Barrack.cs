using System.Collections.Generic;
using UnityEngine;

public class Barrack : ConstructionBase, IDamagable
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    #region Main
    private UI_Controller UI;
    public float MaxHealPoints => healPoints.Capacity;
    public float CurrentHealPoints => healPoints.CurrentValue;
    private ResourceStorage healPoints;
    #endregion

    #region Level-ups
    [SerializeField] private List<BarrackLevel> levels;
    BarrackLevel currentLevel;
    int currentLevelNum = 1;
    
    public float PollenPrice => currentLevel.PollenLevelUpPrice;
    public float WaxPrice => currentLevel.BeesWaxLevelUpPrice;
    public float HousingPrice => currentLevel.HousingLevelUpPrice;
    #endregion

    #region Recruiting
    BeesRecruiting recruiting;
    [SerializeField] private Transform beesSpawnPosition;
    public int RecruitingSize => currentLevel.RecruitingSize;
    #endregion

    protected override void OnAwake()
    {
        base.OnAwake();
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();

        currentLevel = levels[0];

        healPoints = new ResourceStorage(currentLevel.MaxHealPoints,currentLevel.MaxHealPoints);

        recruiting = new BeesRecruiting(currentLevel.RecruitingSize, beesSpawnPosition, currentLevel.BeesRecruitingData);

        _updateEvent += OnUpdate;
    }

    #region RecruitingMethods
    private void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime);
    }

    public string RecruitBees(BeesRecruitingID beeID)
    {
        return recruiting.RecruitBees(beeID);
    }

    public BeeRecruitingInformation GetBeeRecruitingInformation(int n)
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
            Debug.Log("max Barrack level");
            return;
        }
        
        if (ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue >= currentLevel.PollenLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue >= currentLevel.BeesWaxLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue >= currentLevel.HousingLevelUpPrice)
        {
            ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -currentLevel.PollenLevelUpPrice);
            ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, -currentLevel.BeesWaxLevelUpPrice);
            
            currentLevel = levels[currentLevelNum++];
            
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
        }
    }
    public void Repair(float addHP)
    {
        healPoints.ChangeValue(addHP);
    }
    #endregion
}
