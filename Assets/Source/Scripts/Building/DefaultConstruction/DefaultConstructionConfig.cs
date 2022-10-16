using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultConstructionConfig", menuName = "Config/DefaultConstructionConfig")]
public class DefaultConstructionConfig : ScriptableObject, ISingleConfig
{ 
    [SerializeField] private ConstructionConfiguration<DefaultConstruction> _configuration;

    public ConstructionConfiguration<DefaultConstruction> GetConfiguration()
    {
        return _configuration;
    }
}
 