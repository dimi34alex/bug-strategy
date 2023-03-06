using System.Collections.Generic;
using UnityEngine;

public class BeesRecruiting : UnitsRecruitingBase<BeesStack, BeesRecruitingData, BeeRecruitingInformation>
{
    public BeesRecruiting(int size, Transform spawnPos, List<BeesRecruitingData> newDatas) : base(size, spawnPos, newDatas) { }

    public string RecruitBees(BeesRecruitingID beeID)
    {
        int foundStack = Stacks.IndexOf(freeStack => freeStack.Empty == true);

        if (foundStack == -1)
        {
            Debug.Log("Error: All Stack are busy");
            return "All Stack are busy";
        }
        
        BeesRecruitingData foundData = beesRecruitingDatas.Find(fi => fi.CurrentID == beeID);

        if (foundData.PollenPrice <= ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue
            && foundData.HousingPrice <= ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue)
        {
            ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -foundData.PollenPrice);
            ResourceGlobalStorage.ChangeValue(ResourceID.Housing, -foundData.HousingPrice);
            Stacks[foundStack] = new BeesStack(foundData, spawnPosition);
            return null;
        }
        else
            return "Need more resource";
    }

    public override BeeRecruitingInformation GetBeeRecruitingInformation(int n)
    {
        if (n < Stacks.Count && n >= 0)
            return new BeeRecruitingInformation(Stacks[n]);
        else
            Debug.Log("Error: you are trying to get a non-existent BeeRecruitingInformation");

        return new BeeRecruitingInformation(new BeesStack());
    }
}
