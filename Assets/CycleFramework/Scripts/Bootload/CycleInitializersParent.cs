using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[ExecuteAlways]
[DefaultExecutionOrder(-1000)]
public class CycleInitializersParent : MonoBehaviour
{
    private Dictionary<CycleState, CycleInitializersHandler> _initializerHandlers;

    public IReadOnlyDictionary<CycleState, CycleInitializersHandler> CycleInitializersHandlers { get; private set; }

    private void Awake()
    {
        InitInitializers();
    }

    private void InitInitializers()
    {
        CycleInitializersHandlers = GetComponentsInChildren<CycleInitializersHandler>(true)
            .ToDictionary(handler => handler.CycleState, handler => handler);
    }
}
