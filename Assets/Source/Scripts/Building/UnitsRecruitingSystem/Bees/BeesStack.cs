using UnityEngine;

public class BeesStack : UnitStackBase
{    
    public BeesRecruitingID CurrentID { get; protected set; }
    public float PollenPrice { get; protected set; }
    public float HousingPrice { get; protected set; }
    public BeesStack() : base() { }

    public BeesStack(BeesRecruitingData newData, Transform spawnTransform) : base(newData, spawnTransform)
    {
        CurrentID = newData.CurrentID;
        PollenPrice = newData.PollenPrice;
        HousingPrice = newData.HousingPrice;
    }
}