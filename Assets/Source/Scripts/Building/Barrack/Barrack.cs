using System.Collections.Generic;
using UnityEngine;

public class Barrack : ConstructionBase
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    #region Main
    private UI_Controller UI;//контроллер интерфеса
    public float MaxHealPoints => currentLevel.MaxHealPoints;
    public float HealPoints => healPoints;
    protected float healPoints = 0;
    #endregion

    #region Level-ups
    [SerializeField] private List<BarrackLevel> levels;
    BarrackLevel currentLevel;
    int currentLevelNum = 1;
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

        recruiting = new BeesRecruiting(currentLevel.RecruitingSize, beesSpawnPosition, currentLevel.BeesRecruitingData);

        _updateEvent += OnUpdate;
    }

    #region RecruitingMethods
    private void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime);
    }

    public void _RecruitBees(BeesRecruitingID beeID)
    {
        recruiting.RecruitBees(beeID);
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

        if (currentLevelNum == levels.Count)
        {
            Debug.Log("max Barrack level");
            return;
        }
        currentLevel = levels[currentLevelNum++];
        recruiting.AddStacks(currentLevel.RecruitingSize);
        recruiting.SetNewBeesDatas(currentLevel.BeesRecruitingData);
        Debug.Log("Building LVL = " + currentLevelNum);
    }

    public void _GetDamage(float damage)//получение урона
    {
        healPoints -= damage;
        if (healPoints <= 0)
        {
            Destroy(gameObject);
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
