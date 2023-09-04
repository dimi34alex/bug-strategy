using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceSourceBase : MonoBehaviour, IMiniMapShows, ITriggerable, IUnitTarget
{
    public event Action<ITriggerable> OnDestroyITriggerableEvent;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public UnitTargetType TargetType => UnitTargetType.ResourceSource;
    public MiniMapID MiniMapId => MiniMapID.ResourceSource;
    public Transform Transform => transform;

    private void OnDestroy()
    {
        OnDestroyITriggerableEvent?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}