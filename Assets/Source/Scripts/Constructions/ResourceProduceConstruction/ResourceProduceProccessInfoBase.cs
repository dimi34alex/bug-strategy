using System;
using UnityEngine;

[Serializable]
public abstract class ResourceProduceProccessInfoBase
{
    [SerializeField] private ResourceID _targetResourceID;
    [SerializeField] private float _producePerSecond;
    [SerializeField] private int _producedResourceCapacity;

    public ResourceID TargetResourceID => _targetResourceID;
    public float ProducePerSecond => _producePerSecond;
    public int ProducedResourceCapacity => _producedResourceCapacity;
}
