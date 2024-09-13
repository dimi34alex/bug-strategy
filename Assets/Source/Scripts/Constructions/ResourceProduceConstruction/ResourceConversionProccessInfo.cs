using System;
using Source.Scripts.ResourcesSystem;
using UnityEngine;

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