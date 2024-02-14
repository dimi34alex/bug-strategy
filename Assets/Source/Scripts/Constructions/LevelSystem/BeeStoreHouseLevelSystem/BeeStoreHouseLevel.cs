using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeeStoreHouseLevel : BuildingLevelBase
{
    [Header("Resource storages")]
    [SerializeField][Range(0F, 100F)] protected float housingCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float woodCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float leavesCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float pollenCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float waxCapacity = 0;
    [SerializeField][Range(0F, 100F)] protected float honeyCapacity = 0;

    public float HousingCapacity => housingCapacity;
    public float WoodCapacity => woodCapacity;
    public float LeavesCapacity => leavesCapacity;
    public float PollenCapacity => pollenCapacity;
    public float WaxCapacity => waxCapacity;
    public float HoneyCapacity => honeyCapacity;
}