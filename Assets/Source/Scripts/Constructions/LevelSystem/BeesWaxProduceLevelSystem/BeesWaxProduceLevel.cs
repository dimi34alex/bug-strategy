using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BeesWaxProduceLevel : BuildingLevelBase
{
    [SerializeField] private ResourceConversionProccessInfo resourceConversionProccessInfo;
    public ResourceConversionProccessInfo ResourceConversionProccessInfo => resourceConversionProccessInfo;
}
