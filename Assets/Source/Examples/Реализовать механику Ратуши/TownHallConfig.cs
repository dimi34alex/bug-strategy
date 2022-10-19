using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TownHallConfig", menuName = "Config/TownHallConfig")]
public class TownHallConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<TownHall> _configuration;

    public ConstructionConfiguration<TownHall> GetConfiguration()
    {
        return _configuration;
    }
}