using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultConstructionConfig", menuName = "Config/DefaultConstructionConfig")]
public class DefaultConstructionConfig : ScriptableObject, ISingleConfig
{ 
    [SerializeField] private ConstructionSpawnConfiguration<DefaultConstruction> _configuration;

    public ConstructionSpawnConfiguration<DefaultConstruction> GetConfiguration()
    {
        return _configuration;
    }
}
 