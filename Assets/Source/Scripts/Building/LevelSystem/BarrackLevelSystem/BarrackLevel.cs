using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BarrackLevel : BuildingLevelBase
{
    public int RecruitingSize => recruitingSize;
    [SerializeField][Range(1, 6)] int recruitingSize = 1;
    public List<BeesRecruitingData> BeesRecruitingData { get { return beesRecruitingData; } }
    [SerializeField] private List<BeesRecruitingData> beesRecruitingData;
}