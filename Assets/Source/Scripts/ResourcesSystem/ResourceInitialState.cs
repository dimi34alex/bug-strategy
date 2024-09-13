using System;
using UnityEngine;

namespace Source.Scripts.ResourcesSystem
{
    [Serializable]
    public class ResourceInitialState
    {
        [field: SerializeField, Range(0, 10000)] public float Capacity { get; private set; }
        [field: SerializeField, Range(0, 10000)] public float Value { get; private set; }
    }
}