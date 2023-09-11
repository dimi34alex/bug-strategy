using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitsRecruitingSystem;

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
    public float HoneyCapacity => honeyCapacity;
    [SerializeField][Range(0F, 10000F)] protected float honeyCapacity = 0;
    #endregion

    #region Worker Bees
    [Header("Worker bees")]
    [SerializeField][Range(1, 2)] protected int recruitingSize = 0;
    public int RecruitingSize => recruitingSize;
    public List<UnitRecruitingData<BeesRecruitingID>> BeesRecruitingData { get { return new List<UnitRecruitingData<BeesRecruitingID>> { beesRecruitingData }; } }
    [SerializeField] private UnitRecruitingData<BeesRecruitingID> beesRecruitingData;
    #endregion
}