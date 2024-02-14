using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AntStoreHouseLevel : BuildingLevelBase
{
    [Header("Resource storages")]
    [SerializeField][Range(0F, 100F)] protected float housingCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float woodCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float leavesCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float pollenCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float sandCapacity = 0;


    public float HousingCapacity => housingCapacity;
    public float WoodCapacity => woodCapacity;
    public float LeavesCapacity => leavesCapacity;
    public float PollenCapacity => pollenCapacity;
    public float SandCapacity => sandCapacity;
}