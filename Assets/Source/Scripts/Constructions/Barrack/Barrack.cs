using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

public class Barrack : EvolvConstruction<BarrackLevel>
{ 
    public override ConstructionID ConstructionID => ConstructionID.Barrack;
  
    [SerializeField] private Transform beesSpawnPosition;

    private UnitsRecruiter<BeesRecruitingID> _recruiter;
    public IReadOnlyUnitsRecruiter<BeesRecruitingID> Recruiter => _recruiter;
    
    protected override void OnAwake()
    {
        base.OnAwake();

        _recruiter = new UnitsRecruiter<BeesRecruitingID>(CurrentLevel.RecruitingSize, beesSpawnPosition, CurrentLevel.BeesRecruitingData);

        levelSystem = new BarrackLevelSystem(levelSystem, _healthStorage, _recruiter);
        
        _updateEvent += OnUpdate;
    }

    private void OnUpdate()
    {
        _recruiter.Tick(Time.deltaTime);
    }

    public void RecruitBees(BeesRecruitingID beeID)
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
}