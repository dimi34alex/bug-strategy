using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniMapIconBase : MonoBehaviour, IPoolable<MiniMapIconBase, MiniMapID>
{
    public event Action<MiniMapIconBase> ElementReturnEvent;
    public event Action<MiniMapIconBase> ElementDestroyEvent;
    public abstract MiniMapID Identifier { get; }

    public void Return()
    {
        ElementReturnEvent?.Invoke(this);
    }
}