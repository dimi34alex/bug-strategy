using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceSourceBase : MonoBehaviour, IMiniMapShows, ITriggerable, IUnitTarget
{
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;
    public UnitTargetType TargetType => UnitTargetType.ResourceSource;
    public MiniMapID MiniMapId => MiniMapID.ResourceSource;
    public Transform Transform => transform;

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke(this);
    }
}