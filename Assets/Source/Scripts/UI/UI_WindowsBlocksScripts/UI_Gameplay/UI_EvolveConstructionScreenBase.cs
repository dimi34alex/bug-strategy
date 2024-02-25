using System;
using Constructions.LevelSystemCore;
using UnityEngine;

public abstract class UI_EvolveConstructionScreenBase<TConstruction> : UIScreen
    where TConstruction : IEvolveConstruction
{
    protected TConstruction _construction;

    public void _TryLevelUp() => TryLevelUp();
    
    private bool TryLevelUp()
    {
        if (!_construction.LevelSystem.LevelCapCheck())
        {
            UI_Controller._ErrorCall("Max level");
            return false;
        }
        
        if (!_construction.LevelSystem.LevelUpPriceCheck())
        {
            UI_Controller._ErrorCall("Need more resources");
            return false;
        }
        
        LevelUp();
        return true;
    }
    
    private void LevelUp()
    {
        if (_construction.LevelSystem.TryLevelUp())
            Debug.Log($"{_construction.GetType().Name} lvl index = {_construction.LevelSystem.CurrentLevelIndex}");
        else
            throw new Exception($"{_construction.GetType().Name} with lvl index = {_construction.LevelSystem.CurrentLevelIndex}, can't be lvl up-ed");
    }
}