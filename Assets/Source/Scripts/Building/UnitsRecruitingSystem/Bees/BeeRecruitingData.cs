using System;
using UnityEngine;

[Serializable]
public class BeesRecruitingData : UnitRecruitingDataBase
{
    public BeesRecruitingID CurrentID => currentID;
    [SerializeField] private BeesRecruitingID currentID;
}