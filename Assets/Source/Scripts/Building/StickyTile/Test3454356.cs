using System;
using UnityEngine;

public class Test3454356 : MonoBehaviour, ITriggerable
{
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;
}