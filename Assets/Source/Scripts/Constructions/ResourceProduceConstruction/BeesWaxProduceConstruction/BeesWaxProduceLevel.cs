using UnityEngine;
using System;
using Constructions.LevelSystemCore;

namespace Constructions
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