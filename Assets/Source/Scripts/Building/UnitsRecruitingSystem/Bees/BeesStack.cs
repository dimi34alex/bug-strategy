using UnityEngine;

public class BeesStack : UnitStackBase
{    
    public BeesRecruitingID CurrentID { get; protected set; }

    public BeesStack() : base() { }

    public BeesStack(BeesRecruitingData newData, Transform spawnPosition) : base(newData, spawnPosition)
    {
        CurrentID = newData.CurrentID;
    }
}