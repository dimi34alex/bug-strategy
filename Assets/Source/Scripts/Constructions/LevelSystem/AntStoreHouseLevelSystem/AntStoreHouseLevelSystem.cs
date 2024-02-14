using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AntStoreHouseLevelSystem : BuildingLevelSystemBase<AntStoreHouseLevel>
{
    public AntStoreHouseLevelSystem(BuildingLevelSystemBase<AntStoreHouseLevel> buildingLevelSystemBase, ResourceStorage healPoints)
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
        float prevWoodCapacity = CurrentLevel.WoodCapacity;
        float prevLeavesCapacity = CurrentLevel.LeavesCapacity;
        float prevPollenCapacity = CurrentLevel.PollenCapacity;
        float prevSandCapacity = CurrentLevel.SandCapacity;
        currentLevelNum++;
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity - prevHousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity - prevHousingCapacity); 
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Wood, CurrentLevel.WoodCapacity - prevWoodCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Wood,CurrentLevel.WoodCapacity - prevWoodCapacity);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Leaves, CurrentLevel.LeavesCapacity - prevLeavesCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Leaves,CurrentLevel.LeavesCapacity - prevLeavesCapacity);

        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity - prevPollenCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, CurrentLevel.PollenCapacity - prevPollenCapacity);

        ResourceGlobalStorage.ChangeCapacity(ResourceID.Ants_Sand, CurrentLevel.SandCapacity - prevSandCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Ants_Sand, CurrentLevel.SandCapacity - prevSandCapacity);


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