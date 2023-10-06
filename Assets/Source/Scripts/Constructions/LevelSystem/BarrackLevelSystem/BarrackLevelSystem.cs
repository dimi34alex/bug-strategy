using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnitsRecruitingSystem;

[Serializable]
public class BarrackLevelSystem : BuildingLevelSystemBase<BarrackLevel>
{
    private BeesRecruiting _beesRecruiting;
    
    public BarrackLevelSystem(BuildingLevelSystemBase<BarrackLevel> buildingLevelSystemBase, ResourceStorage healPoints,
        BeesRecruiting beesRecruiting) : base(buildingLevelSystemBase, healPoints)
    {
        _beesRecruiting = beesRecruiting;
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
        currentLevelNum++;
            
        _beesRecruiting.AddStacks(CurrentLevel.RecruitingSize);
        _beesRecruiting.SetNewDatas(CurrentLevel.BeesRecruitingData);

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
