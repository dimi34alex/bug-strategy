using System.Collections.Generic;
using UnityEngine;

public class BeesRecruiting : UnitsRecruitingBase<BeesStack, BeesRecruitingData, BeeRecruitingInformation>
{
    public BeesRecruiting(int size, Transform spawnPos, List<BeesRecruitingData> newDatas) : base(size, spawnPos, newDatas) { }

    public string RecruitBees(BeesRecruitingID beeID)
    {
        int findedStack = Stacks.IndexOf(freeStack => freeStack.Empty == true);

        if (findedStack == -1)
        {
            Debug.Log("Error: All Stack are busy");
            return "All Stack are busy";
        }
        
        BeesRecruitingData findedData = beesRecruitingDatas.Find(fi => fi.CurrentID == beeID);

        if (findedData.PollenPrice <= ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue)
        {
            Stacks[findedStack] = new BeesStack(findedData, spawnPosition);
            return null;
        }
        else
            return "Need more resource";
    }

    public override BeeRecruitingInformation GetBeeRecruitingInformation(int n)
    {
        if (n < Stacks.Count)
            return new BeeRecruitingInformation(Stacks[n]);
        else
            Debug.Log("Error: are you trying to get a non-existent BeeRecruitingInformation");

        return new BeeRecruitingInformation(Stacks[0]);
    }
}
