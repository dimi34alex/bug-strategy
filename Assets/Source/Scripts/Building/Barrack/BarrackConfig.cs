using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BarrackConfig", menuName = "Config/BarrackConfig")]
public class BarrackConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<Barrack> _configuration;

    public ConstructionConfiguration<Barrack> GetConfiguration()
    {
        return _configuration;
    }
}