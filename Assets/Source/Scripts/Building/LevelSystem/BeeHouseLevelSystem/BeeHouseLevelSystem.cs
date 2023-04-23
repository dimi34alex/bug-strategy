using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeeHouseLevelSystem : BuildingLevelSystemBase<BeeHouseLevel>
{
    public BeeHouseLevelSystem(BuildingLevelSystemBase<BeeHouseLevel> buildingLevelSystemBase, ResourceStorage healPoints)
        : base(buildingLevelSystemBase, healPoints)
    { }
    
    public override void NextLevel()
    {
        try
        {
            base.NextLevel();
        }
        catch (Exception e)
        {
            UI_Controller._ErrorCall(e.Message);
            return;
        }
        
        SpendResources();
        float prevHousingCapacity = CurrentLevel.HousingCapacity;
        currentLevelNum++;
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity - prevHousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity - prevHousingCapacity);
            
        if (HealPoints.CurrentValue >= HealPoints.Capacity)
        {
            HealPoints.SetCapacity(CurrentLevel.MaxHealPoints);
            HealPoints.SetValue(CurrentLevel.MaxHealPoints);
        }
        else
        {
            HealPoints.SetCapacity(CurrentLevel.MaxHealPoints);
        }

        Debug.Log("Building LVL = " + CurrentLevelNum);
    }
}
