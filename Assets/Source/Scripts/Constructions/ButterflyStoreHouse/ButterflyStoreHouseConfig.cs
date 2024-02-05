using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AntStoreHouseConfig", menuName = "Config/AntStoreHouseConfig")]
public class ButterflyStoreHouseConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<ButterflyStoreHouse> _configuration;

    public ConstructionConfiguration<ButterflyStoreHouse> GetConfiguration()
    {
        return _configuration;
    }
}