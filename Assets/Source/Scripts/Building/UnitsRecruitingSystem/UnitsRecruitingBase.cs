using System.Collections.Generic;
using UnityEngine;

public abstract class UnitsRecruitingBase<TUnitStack, TUnitRecruitingData, TUnitRecruitingInformation> 
    where TUnitStack : UnitStackBase, new()
    where TUnitRecruitingData : UnitRecruitingDataBase, new()
    where TUnitRecruitingInformation : UnitRecruitingInformationBase, new()
{
    protected Transform spawnPosition;
    protected List<TUnitStack> Stacks;
    protected List<TUnitRecruitingData> beesRecruitingDatas;
    protected int currentStack = 0;

    public UnitsRecruitingBase(int size, Transform spawnPos, List<TUnitRecruitingData> newDatas)
    {
        Stacks = new List<TUnitStack>(new TUnitStack[size]);
        beesRecruitingDatas = newDatas;
        spawnPosition = spawnPos;

        for (int n = 0; n < size; n++)
            Stacks[n] = new TUnitStack();
    }

    public void SetNewBeesDatas(List<TUnitRecruitingData> newDatas)
    {
        beesRecruitingDatas = newDatas;
    }
    public void AddStacks(int newSize)
    {
        for (int n = Stacks.Count; n < newSize; n++)
        {
            Stacks.Add(new TUnitStack());
        }
    }

    public void Tick(float time, AffiliationEnum team)
    {
        foreach (TUnitStack mass in Stacks)
            if (!mass.Empty)
                mass.StackTick(time, team);

        currentStack++;

        if (currentStack >= Stacks.Count)
            currentStack = 0;
    }

    public abstract List<TUnitRecruitingInformation> GetRecruitingInformation();
}
