using ConstructionLevelSystem;
using UnityEngine;

public abstract class EvolvConstruction<TConstructionLevel> : ConstructionBase
    where TConstructionLevel : ConstructionLevelBase
{
    [SerializeField] protected ConstructionLevelSystemBase<TConstructionLevel> levelSystem;
    [SerializeField] private SerializableDictionary<ResourceID, int> _costValues;

    protected TConstructionLevel CurrentLevel => levelSystem.CurrentLevel;
    public override Cost Cost => _cost;

    private Cost _cost;

    protected override void OnAwake()
    {
        CalculateCost();
        base.OnAwake();
    }

    public override void CalculateCost()
    {
        _cost = new Cost(_costValues);
    }

    public void LevelUp()
    {
        if (levelSystem.LevelCapCheck())
        {
            UI_Controller._ErrorCall("Max building level");
            return;
        }
        
        if (!levelSystem.PriceCheck())
        {
            UI_Controller._ErrorCall("Need more resources");
            return;
        }
        
        levelSystem.TryLevelUp();
        
        Debug.Log("Construction LVL = " + levelSystem.CurrentLevelNum);
    }
}
