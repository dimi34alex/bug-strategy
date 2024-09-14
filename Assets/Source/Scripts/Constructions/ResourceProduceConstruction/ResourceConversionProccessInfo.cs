using System;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction
{
    [Serializable]
    public class ResourceConversionProccessInfo : ResourceProduceProccessInfoBase
    {
        [SerializeField] private ResourceID _spendableResourceID;
        [SerializeField] private float _spendRatio;
        [SerializeField] private int _spendableResourceCapacity;

        public ResourceID SpendableResourceID => _spendableResourceID;
        public float SpendRatio => _spendRatio;
        public int SpendableResourceCapacity => _spendableResourceCapacity;
    }
}