using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction
{
    [Serializable]
    public class BeesWaxProduceLevel : ConstructionLevelBase
    {
        [SerializeField] private ResourceConversionProccessInfo resourceConversionProccessInfo;
        [field: Space] 
        [field: SerializeField] [field: Range(0, 10)] public int HiderCapacity { get; private set; }
        
        public ResourceConversionProccessInfo ResourceConversionProccessInfo => resourceConversionProccessInfo;
    }
}