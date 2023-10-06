using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeeHouseLevel : BuildingLevelBase
{
    [Header("Resource storages")]
    [SerializeField][Range(0F, 100F)] protected float housingCapacity = 0;

    public float HousingCapacity => housingCapacity;
}
