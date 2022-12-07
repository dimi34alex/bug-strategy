using System;
using UnityEngine;

[Serializable]
public class BeesRecruitingData : UnitRecruitingDataBase
{
    public BeesRecruitingID CurrentID => currentID;
    [SerializeField] private BeesRecruitingID currentID;
    public float PollenPrice => pollenPrice;
    [SerializeField] private float pollenPrice;
    public float HousingPrice => housingPrice;
    [SerializeField] private float housingPrice;
}