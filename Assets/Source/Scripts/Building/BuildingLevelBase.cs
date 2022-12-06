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
    [SerializeField][Range(0F, 10000F)] float pollenLevelUpPrice = 0;//воск
    public float PollenLevelUpPrice => pollenLevelUpPrice;
    public float BeesWaxLevelUpPrice => bees_waxLevelUpPrice;
    [SerializeField][Range(0F, 10000F)] float bees_waxLevelUpPrice = 0;//воск
}
