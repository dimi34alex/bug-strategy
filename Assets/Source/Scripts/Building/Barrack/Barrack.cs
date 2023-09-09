using System.Collections.Generic;
using UnityEngine;

public class Barrack : EvolvConstruction<BarrackLevel>
{ 
    public override ConstructionID ConstructionID => ConstructionID.Barrack;
  
    BeesRecruiting recruiting;
    [SerializeField] private Transform beesSpawnPosition;
    public int RecruitingSize => CurrentLevel.RecruitingSize;

    public Affiliation team;

    protected override void OnAwake()
    {
        base.OnAwake();

        recruiting = new BeesRecruiting(CurrentLevel.RecruitingSize, beesSpawnPosition, CurrentLevel.BeesRecruitingData);

        levelSystem = new BarrackLevelSystem(levelSystem, _healthStorage, recruiting);
        
        _updateEvent += OnUpdate;
    }

    private void OnUpdate()
    {
        recruiting.Tick(Time.deltaTime, team.affiliation);
    }

    public void RecruitBees(BeesRecruitingID beeID)
    {
        
        recruiting.RecruitBees(beeID);
    }
    
    public List<BeeRecruitingInformation> GetRecruitingInformation()
    {
        return recruiting.GetRecruitingInformation();
    }
}