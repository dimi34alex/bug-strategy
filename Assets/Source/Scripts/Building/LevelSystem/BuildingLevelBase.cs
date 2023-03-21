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
    
    [Header("Prices to next level")]
    [SerializeField][Range(0F, 10000F)] float pollenLevelUpPrice = 0;
    public float PollenLevelUpPrice => pollenLevelUpPrice;
    public float BeesWaxLevelUpPrice => bees_waxLevelUpPrice;
    [SerializeField][Range(0F, 10000F)] float bees_waxLevelUpPrice = 0;
    public float HousingLevelUpPrice => housingLevelUpPrice;
    [SerializeField][Range(0F, 10000F)] float housingLevelUpPrice = 0;
    public float HoneyLevelUpPrice => honeyLevelUpPrice;
    [SerializeField][Range(0F, 10000F)] float honeyLevelUpPrice = 0;
}
