using System;
using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction
{
    public abstract ConstructionID ConstructionID { get; }

    protected event Action _updateEvent;

    protected void Awake()
    {
        OnAwake();
    }

    protected void Start() => OnStart();
    protected void Update() => _updateEvent?.Invoke();

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
}
