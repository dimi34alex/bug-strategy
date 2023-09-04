using System;
using System.Collections.Generic;
using UnityEngine;
public class VisibleWarFogZone : MonoBehaviour, ITriggerable
{
    public event Action<ITriggerable> OnDestroyITriggerableEvent;
    public event Action<ITriggerable> OnDisableITriggerableEvent;

    private void OnDestroy()
    {
        OnDestroyITriggerableEvent?.Invoke(this);
    }
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}