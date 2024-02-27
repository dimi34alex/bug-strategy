using UnityEngine;
using System;
using MiniMapSystem;

public abstract class ResourceSourceBase : MonoBehaviour, IMiniMapObject, ITriggerable, IUnitTarget
{
    public abstract ResourceID ResourceID { get; }
    public bool IsActive { get; protected set; } = true;
    
    public AffiliationEnum Affiliation => AffiliationEnum.None;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.ResourceSource;
    public UnitTargetType TargetType => UnitTargetType.ResourceSource;
    public Transform Transform => transform;
    
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnDeactivation;
    
    public abstract void ExtractResource(int extracted);
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
        OnDeactivation?.Invoke();
    }
}