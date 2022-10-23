using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TownHallLevel : BuildingLevelBase
{
    #region Resource storages
    [Header("Resource storages")]
    [SerializeField][Range(0F, 10000F)] protected float maxTrees = 0;
    public float MaxTrees => maxTrees;
    public float MaxFlowers => maxFlowers;
    [SerializeField][Range(0F, 10000F)] protected float maxFlowers = 0;
    public float MaxPlants => maxPlants;
    [SerializeField][Range(0F, 10000F)] protected float maxPlants = 0;
    public float MaxWax => maxWax;
    [SerializeField][Range(0F, 10000F)] protected float maxWax = 0;
    #endregion

    #region Worker Bees
    [Header("Worker bees")]
    [SerializeField][Range(0, 50)] protected int maxWorkerBeesNumber = 0;
    public int MaxWorkerBeesNumber => maxWorkerBeesNumber;
    public int MaxWorkerBeesQueue => maxWorkerBeesQueue;
    [SerializeField][Range(0, 20)] protected int maxWorkerBeesQueue = 0;
    public float TimeWorkerBeeBuild => timeWorkerBeeBuild;
    [SerializeField][Range(0F, 120F)] protected float timeWorkerBeeBuild = 0;
    #endregion
}