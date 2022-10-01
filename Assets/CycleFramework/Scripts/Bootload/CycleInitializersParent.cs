using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[ExecuteAlways]
public class CycleInitializersParent : MonoBehaviour
{
    private readonly BindingFlags _defaultFlag = BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic;
    private Dictionary<CycleState, CycleInitializersHandler> _initializerHandlers;
    private object _flowLocker;

    public IReadOnlyDictionary<CycleState, CycleInitializersHandler> CycleInitializersHandlers { get; private set; }

    private void OnEnable()
    {
        TryInitHierarсhy();

        if (Application.isPlaying)
            InitInitializers();
    }

    private void OnTransformChildrenChanged() => TryInitHierarсhy();
    private void OnTransformParentChanged() => TryInitHierarсhy();

    private void InitInitializers()
    {
        CycleInitializersHandlers = GetComponentsInChildren<CycleInitializersHandler>(true)
            .ToDictionary(handler => handler.CycleState, handler => handler);
    }

    private void TryInitHierarсhy()
    {
        if (_flowLocker != null)
            return;

        _flowLocker = new object();

        CycleState[] states = (CycleState[])Enum.GetValues(typeof(CycleState));

        if (_initializerHandlers != null && _initializerHandlers.Count == states.Length)
            goto MethodEnd;

        _initializerHandlers = new Dictionary<CycleState, CycleInitializersHandler>(states.Length);

        CycleInitializersHandler[] handlers = GetComponentsInChildren<CycleInitializersHandler>(true);

        foreach (CycleInitializersHandler handler in handlers)
            _initializerHandlers.Add(handler.CycleState, handler);

        foreach (CycleState cycleState in states)
        {
            if (!_initializerHandlers.ContainsKey(cycleState))
            {
                CycleInitializersHandler handler = new GameObject($"[{nameof(CycleState)}] {cycleState}")
                    .AddComponent<CycleInitializersHandler>();

                FieldInfo stateField = handler.GetType().GetFields(_defaultFlag).Find(field => field.FieldType == typeof(CycleState));
                stateField.SetValue(handler, cycleState);
                handler.transform.parent = transform;

                _initializerHandlers.Add(cycleState, handler);
            }
        }

    MethodEnd:

        _flowLocker = null;
    }
}
