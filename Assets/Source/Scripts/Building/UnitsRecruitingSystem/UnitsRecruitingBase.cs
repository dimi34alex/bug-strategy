using System.Collections.Generic;
using UnityEngine;

public abstract class UnitsRecruitingBase<T, K, S> where T : UnitStackBase, new()
                                        where K : UnitRecruitingDataBase, new()
                                        where S : UnitRecruitingInformationBase, new()
{
    protected Transform spawnPosition;
    protected List<T> Stacks;
    protected List<K> beesRecruitingDatas;
    protected int currentStack = 0;

    public UnitsRecruitingBase(int size, Transform spawnPos, List<K> newDatas)
    {
        Stacks = new List<T>(new T[size]);
        beesRecruitingDatas = newDatas;
        spawnPosition = spawnPos;

        for (int n = 0; n < size; n++)
            Stacks[n] = new T();
    }

    public void _SetNewBeesDatas(List<K> newDatas)
    {
        beesRecruitingDatas = newDatas;
    }
    public void _AddStacks(int newSize)
    {
        for (int n = Stacks.Count; n < newSize; n++)
        {
            Stacks.Add(new T());
        }
    }

    public void Tick(float time)
    {
        if (!Stacks[currentStack].Empty) { }
        Stacks[currentStack].StackTick(time);

        currentStack++;

        if (currentStack >= Stacks.Count)
            currentStack = 0;
    }

    abstract public S GetBeeRecruitingInformation(int n);
}
