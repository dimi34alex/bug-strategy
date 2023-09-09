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
    public override Cost Cost => _cost;

    protected override void OnAwake()
    {
        CalculateCost();
        base.OnAwake();
        
        _healthStorage = new ResourceStorage(CurrentLevel.MaxHealPoints,CurrentLevel.MaxHealPoints);
    }

    public override void CalculateCost()
    {
        _cost = new Cost(_costValues);
    }

    public void NextBuildingLevel()
    {
        levelSystem.NextLevel();
    }
}
