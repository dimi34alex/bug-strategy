using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = nameof(ResourceGlobalStorageConfig), menuName = "Configs/" + nameof(ResourceGlobalStorageConfig))]
public class ResourceGlobalStorageConfig : ScriptableObject, ISingleConfig
{
    [SerializeField]
    private SerializableDictionary<AffiliationEnum, SerializableDictionary<ResourceID, ResourceInitialState>> initialStates;

    public IReadOnlyDictionary<AffiliationEnum, IReadOnlyDictionary<ResourceID, ResourceInitialState>> InitialStates => 
        initialStates.ToDictionary(pair => pair.Key, pair => pair.Value as IReadOnlyDictionary<ResourceID, ResourceInitialState>);
}

[Serializable]
public class ResourceInitialState
{
    [field: SerializeField, Range(0, 10000)] public float Capacity { get; private set; }
    [field: SerializeField, Range(0, 10000)] public float Value { get; private set; }
}
