using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildingLevelSystemBase <TBuildingLevel> where TBuildingLevel : BuildingLevelBase
{
    [SerializeField] protected List<TBuildingLevel> levels;
    public TBuildingLevel CurrentLevel => levels[currentLevelNum]; 
    public int CurrentLevelNum => currentLevelNum + 1;
    protected int currentLevelNum;
    protected ResourceStorage HealPoints;

    protected BuildingLevelSystemBase(BuildingLevelSystemBase<TBuildingLevel> buildingLevelSystemBase,  ResourceStorage healPoints)
    {
        levels = buildingLevelSystemBase.levels;
        currentLevelNum = 0;
        HealPoints = healPoints;
    }

    public virtual void NextLevel()
    {
        if (CurrentLevelNum == levels.Count)
        {
            throw new Exception("max building level");
        }

        if (!PriceCheck())
        {
            throw new Exception("Need more resources");
        }
    }

    protected bool PriceCheck()
    {
        if (ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue >= CurrentLevel.PollenLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue >= CurrentLevel.BeesWaxLevelUpPrice
            && ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue >= CurrentLevel.HousingLevelUpPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void SpendResources()
    {
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -CurrentLevel.PollenLevelUpPrice);
        ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, -CurrentLevel.BeesWaxLevelUpPrice);
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing, -CurrentLevel.HousingLevelUpPrice);
    }
}
