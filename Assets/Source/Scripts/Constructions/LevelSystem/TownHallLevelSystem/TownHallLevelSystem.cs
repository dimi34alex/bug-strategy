using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnitsRecruitingSystem;

[Serializable]
public class TownHallLevelSystem : BuildingLevelSystemBase <TownHallLevel>
{
    private UnitsRecruiter<BeesRecruitingID> _recruiter;
    
    public TownHallLevelSystem(BuildingLevelSystemBase<TownHallLevel> buildingLevelSystemBase, ResourceStorage healPoints,
        UnitsRecruiter<BeesRecruitingID> recruiter) : base(buildingLevelSystemBase, healPoints)
    {
        _recruiter = recruiter;
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, buildingLevelSystemBase.CurrentLevel.PollenCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, buildingLevelSystemBase.CurrentLevel.BeesWaxCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, buildingLevelSystemBase.CurrentLevel.HousingCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Honey, buildingLevelSystemBase.CurrentLevel.HoneyCapacity);
        
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing, buildingLevelSystemBase.CurrentLevel.HousingCapacity);
    }

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
        float prevPollenCapacity = CurrentLevel.PollenCapacity;
        float prevBeesWaxCapacity = CurrentLevel.BeesWaxCapacity;
        float prevHousingCapacity = CurrentLevel.HousingCapacity;
        float prevHoneyCapacity = CurrentLevel.HoneyCapacity;
        currentLevelNum++;
            
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity - prevPollenCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, CurrentLevel.BeesWaxCapacity - prevBeesWaxCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity - prevHousingCapacity);
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Honey, CurrentLevel.HoneyCapacity - prevHoneyCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity - prevHousingCapacity);
            
        _recruiter.AddStacks(CurrentLevel.RecruitingSize);
        _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);

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
