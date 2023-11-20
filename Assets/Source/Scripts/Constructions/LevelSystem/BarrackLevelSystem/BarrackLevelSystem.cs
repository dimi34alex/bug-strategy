using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnitsRecruitingSystem;

[Serializable]
public class BarrackLevelSystem : BuildingLevelSystemBase<BarrackLevel>
{
    private UnitsRecruiter<BeesRecruitingID> _recruiter;
    
    public BarrackLevelSystem(BuildingLevelSystemBase<BarrackLevel> buildingLevelSystemBase, ResourceStorage healPoints,
        UnitsRecruiter<BeesRecruitingID> recruiter) : base(buildingLevelSystemBase, healPoints)
    {
        _recruiter = recruiter;
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
