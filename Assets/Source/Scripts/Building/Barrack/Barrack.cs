using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

public class Barrack : EvolvConstruction<BarrackLevel>
{ 
    public override ConstructionID ConstructionID => ConstructionID.Barrack;
  
    [SerializeField] private Transform beesSpawnPosition;
    BeesRecruiting recruiting;
    public int RecruitingSize => CurrentLevel.RecruitingSize;

    public Affiliation team;

    protected override void OnAwake()
    {
        base.OnAwake();

        recruiting = new BeesRecruiting(CurrentLevel.RecruitingSize, beesSpawnPosition, AffiliationEnum.Team1, CurrentLevel.BeesRecruitingData);

        levelSystem = new BarrackLevelSystem(levelSystem, _healthStorage, recruiting);
        
        _updateEvent += OnUpdate;
    }

    private void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime);
    }

    public void RecruitBees(BeesRecruitingID beeID)
    {
        recruiting.RecruitUnit(beeID, out string errorLog);
        if(errorLog.Length > 0) UI_Controller._ErrorCall(errorLog);
    }
    
    public List<IReadOnlyUnitRecruitingStack<BeesRecruitingID>> GetRecruitingInformation()
    {
        return recruiting.GetRecruitingInformation();
    }
}