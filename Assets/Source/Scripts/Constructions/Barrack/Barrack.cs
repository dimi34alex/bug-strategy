using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

public class Barrack : EvolvConstruction<BarrackLevel>
{ 
    public override ConstructionID ConstructionID => ConstructionID.Barrack;
  
    [SerializeField] private Transform beesSpawnPosition;

    private BeesRecruiting _recruiting;
    public IReadOnlyUnitsRecruiting<BeesRecruitingID> Recruiting => _recruiting;
    
    protected override void OnAwake()
    {
        base.OnAwake();

        _recruiting = new BeesRecruiting(CurrentLevel.RecruitingSize, beesSpawnPosition, CurrentLevel.BeesRecruitingData);

        levelSystem = new BarrackLevelSystem(levelSystem, _healthStorage, _recruiting);
        
        _updateEvent += OnUpdate;
    }

    private void OnUpdate()
    {
        _recruiting.Tick(Time.deltaTime);
    }

    public void RecruitBees(BeesRecruitingID beeID)
    {
        _recruiting.RecruitUnit(beeID, out string errorLog);
        if(errorLog.Length > 0) UI_Controller._ErrorCall(errorLog);
    }
}