using UnityEngine;
using System;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeesWaxProduceLevel : ConstructionLevelBase
    {
        [SerializeField] private ResourceConversionProccessInfo resourceConversionProccessInfo;

        public ResourceConversionProccessInfo ResourceConversionProccessInfo => resourceConversionProccessInfo;
    }
}