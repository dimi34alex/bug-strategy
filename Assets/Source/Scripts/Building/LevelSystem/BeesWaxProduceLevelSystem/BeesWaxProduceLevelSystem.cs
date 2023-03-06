using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeesWaxProduceLevelSystem : BuildingLevelSystemBase<BeesWaxProduceLevel>
{
    private ResourceConversionCore _resourceConversionCore;
    
    public BeesWaxProduceLevelSystem(BuildingLevelSystemBase<BeesWaxProduceLevel> buildingLevelSystemBase, ResourceStorage healPoints,
        ResourceConversionCore resourceConversionCore) : base(buildingLevelSystemBase, healPoints)
    {
        _resourceConversionCore = resourceConversionCore;
    }
    
    public override void NextLevel()
    {
        try
        {
            base.NextLevel();
        }
        catch (Exception e)
        {
            
            return;
        }
        
        SpendResources();
        currentLevelNum++;

        _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
            
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
