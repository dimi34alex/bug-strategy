using UnityEngine;
using System;
using MiniMapSystem;

public class ResourceSourceBase : MonoBehaviour, IMiniMapObject, ITriggerable, IUnitTarget
{
    public AffiliationEnum Affiliation => AffiliationEnum.None;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.ResourceSource;
    
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public UnitTargetType TargetType => UnitTargetType.ResourceSource;

    public Transform Transform => transform;
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}