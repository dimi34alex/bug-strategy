using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvolvConstruction<TBuildingLevel> : ConstructionBase
    where TBuildingLevel : BuildingLevelBase
{
    [SerializeField] protected BuildingLevelSystemBase<TBuildingLevel> levelSystem;
    protected TBuildingLevel CurrentLevel => levelSystem.CurrentLevel;
    public int CurrentLevelNum => levelSystem.CurrentLevelNum;
    [SerializeField] private SerializableDictionary<ResourceID, int> _costValues;
    private Cost _cost;
    public  Cost Cost => _cost;

    protected override void OnAwake()
    {
        _cost = new Cost(_costValues);
        base.OnAwake();
        
        HealPoints = new ResourceStorage(CurrentLevel.MaxHealPoints,CurrentLevel.MaxHealPoints);
    }

    public void NextBuildingLevel()
    {
        levelSystem.NextLevel();
    }
}
