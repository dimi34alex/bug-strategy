using System;
using UnityEngine;

public interface ITriggerable
{
    public event Action<ITriggerable> OnDisableITriggerableEvent;
}
