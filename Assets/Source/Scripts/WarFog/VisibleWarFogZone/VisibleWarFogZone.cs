using System;
using System.Collections.Generic;
using UnityEngine;
public class VisibleWarFogZone : MonoBehaviour, ITriggerable
{
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
    
    private void OnDisable()
    {
        OnDisableEvent?.Invoke(this);
    }
}