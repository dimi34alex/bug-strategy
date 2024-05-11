using UnityEngine;
using System;
using MiniMapSystem;

public abstract class ResourceSourceBase : MonoBehaviour, IMiniMapObject, ITriggerable, IUnitTarget
{
    [SerializeField] private int resourceCapacity;
    
    protected ResourceStorage ResourceStorage;
    
    public abstract ResourceID ResourceID { get; }
    public bool IsActive { get; protected set; } = true;
    public bool CanBeCollected { get; protected set; } = true;
    
    public AffiliationEnum Affiliation => AffiliationEnum.None;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.ResourceSource;
    public UnitTargetType TargetType => UnitTargetType.ResourceSource;
    public Transform Transform => transform;
    
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnDeactivation;

    private void Awake()
    {
        ResourceStorage = new ResourceStorage(resourceCapacity, resourceCapacity);
    }

    protected virtual void OnAwake(){}
    
    public abstract void ExtractResource(int extracted);
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}