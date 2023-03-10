using System;
using System.Collections.Generic;
using UnityEngine;

public class BeesRecruiting : UnitsRecruitingBase<BeesStack, BeesRecruitingData, BeeRecruitingInformation>
{
    public BeesRecruiting(int size, Transform spawnPos, List<BeesRecruitingData> newDatas) : base(size, spawnPos, newDatas) { }

    public void RecruitBees(BeesRecruitingID beeID)
    {
        try
        {
            int foundStack = Stacks.IndexOf(freeStack => freeStack.Empty == true);

            if (foundStack == -1)
                throw new Exception("All Stack are busy");

            BeesRecruitingData foundData = beesRecruitingDatas.Find(fi => fi.CurrentID == beeID);

            if (foundData.PollenPrice <= ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue
                && foundData.HousingPrice <= ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue)
            {
                ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -foundData.PollenPrice);
                ResourceGlobalStorage.ChangeValue(ResourceID.Housing, -foundData.HousingPrice);
                Stacks[foundStack] = new BeesStack(foundData, spawnPosition);
            }
            else
            {
                throw new Exception("Need more resource");
            }
        }
        catch (Exception e)
        {
            UI_Controller._ErrorCall(e.Message);
        }

    }
    
    public override List<BeeRecruitingInformation> GetRecruitingInformation()
    {
        List<BeeRecruitingInformation> fullInformation = new List<BeeRecruitingInformation>();

        foreach (var stack in Stacks)
        {
            fullInformation.Add(new BeeRecruitingInformation(stack));
        }
        
        return fullInformation;
    }
}
