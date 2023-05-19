using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvolvConstruction<TBuildingLevel> : ConstructionBase
    where TBuildingLevel : BuildingLevelBase
{
    [SerializeField] protected BuildingLevelSystemBase<TBuildingLevel> levelSystem;
    protected TBuildingLevel CurrentLevel => levelSystem.CurrentLevel;
    public int CurrentLevelNum => levelSystem.CurrentLevelNum;

    protected override void OnAwake()
    {
        base.OnAwake();
        
        HealPoints = new ResourceStorage(CurrentLevel.MaxHealPoints,CurrentLevel.MaxHealPoints);
    }

    public void NextBuildingLevel()
    {
        levelSystem.NextLevel();
    }
}
