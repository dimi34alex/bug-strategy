using System;
using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

[Serializable]
public class BarrackLevel : BuildingLevelBase
{
    [SerializeField][Range(1, 6)] int recruitingSize = 1;
    [SerializeField] private List<UnitRecruitingData<BeesRecruitingID>> beesRecruitingData;

    public int RecruitingSize => recruitingSize;
    public List<UnitRecruitingData<BeesRecruitingID>> BeesRecruitingData { get { return beesRecruitingData; } }
}