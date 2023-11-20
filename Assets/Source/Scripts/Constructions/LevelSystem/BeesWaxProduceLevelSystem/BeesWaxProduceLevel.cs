using UnityEngine;
using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BeesWaxProduceLevel : ConstructionLevelBase
    {
        [SerializeField] private ResourceConversionProccessInfo resourceConversionProccessInfo;

        public ResourceConversionProccessInfo ResourceConversionProccessInfo => resourceConversionProccessInfo;
    }
}