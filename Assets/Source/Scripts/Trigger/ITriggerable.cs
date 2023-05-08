using System;
using UnityEngine;

public interface ITriggerable
{
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;
}
