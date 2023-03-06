using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TownHallLevel : BuildingLevelBase
{
    #region Resource storages
    [Header("Resource storages")]
    [SerializeField][Range(0F, 10000F)] protected float pollenCapacity = 0;
    public float PollenCapacity => pollenCapacity;
    public float BeesWaxCapacity => bees_waxCapacity;
    [SerializeField][Range(0F, 10000F)] protected float bees_waxCapacity = 0;
    public float HousingCapacity => housingCapacity;
    [SerializeField][Range(0F, 100F)] protected float housingCapacity = 0;
    #endregion

    #region Worker Bees
    [Header("Worker bees")]
    [SerializeField][Range(1, 2)] protected int recruitingSize = 0;
    public int RecruitingSize => recruitingSize;
    public List<BeesRecruitingData> BeesRecruitingData { get { return new List<BeesRecruitingData> { beesRecruitingData }; } }
    [SerializeField] private BeesRecruitingData beesRecruitingData;
    #endregion
}