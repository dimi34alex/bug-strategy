using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingLevelBase
{
    [Header("Main")]
    [SerializeField][Range(0F, 100000F)] protected float maxHealPoints = 0;
    public float MaxHealPoints => maxHealPoints;
    
    //цена в кол-ве ресурсов для повышения до следующего лвл-а ратуши
    [Header("Prices to next level")]
    [SerializeField][Range(0F, 10000F)] float treesPrice = 0;
    public float TreesPrice => treesPrice;
    public float FlowersPrice => flowersPrice;
    [SerializeField][Range(0F, 10000F)] float flowersPrice = 0;
    public float PlantsPrice => plantsPrice;
    [SerializeField][Range(0F, 10000F)] float plantsPrice = 0;
    public float WaxPrice => waxPrice;
    [SerializeField][Range(0F, 10000F)] float waxPrice = 0;//воск
}
