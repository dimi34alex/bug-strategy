using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeeStoreHouseLevelSystem : BuildingLevelSystemBase<BeeStoreHouseLevel>
{
    public BeeStoreHouseLevelSystem(BuildingLevelSystemBase<BeeStoreHouseLevel> buildingLevelSystemBase, ResourceStorage healPoints)
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
        float prevWaxCapacity = CurrentLevel.WaxCapacity;
        float prevHoneyCapacity = CurrentLevel.HoneyCapacity;
        currentLevelNum++;
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity - prevHousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity - prevHousingCapacity); 
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Wood, CurrentLevel.WoodCapacity - prevWoodCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Wood,CurrentLevel.WoodCapacity - prevWoodCapacity);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Leaves, CurrentLevel.LeavesCapacity - prevLeavesCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Leaves,CurrentLevel.LeavesCapacity - prevLeavesCapacity);

        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity - prevPollenCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, CurrentLevel.PollenCapacity - prevPollenCapacity);
                    
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, CurrentLevel.WaxCapacity - prevWaxCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, CurrentLevel.WaxCapacity - prevWaxCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Honey, CurrentLevel.HoneyCapacity - prevHoneyCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Honey, CurrentLevel.HoneyCapacity - prevHoneyCapacity);


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