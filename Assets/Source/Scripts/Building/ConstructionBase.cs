using System;
using System.Collections.Generic;
using UnityEngine;
using MiniMapSystem;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable, IMiniMapObject,
    ITriggerable, IUnitTarget, SelectableSystem.ISelectable, IAffiliation
{
    [SerializeField] private AffiliationEnum affiliationEnum;

    public abstract ConstructionID ConstructionID { get; }
    public UnitTargetType TargetType => UnitTargetType.Construction;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.Construction;
    public virtual Cost Cost { get; }
    public Transform Transform => transform;

    protected ResourceStorage _healthStorage;
    public IReadOnlyResourceStorage HealthStorage => _healthStorage;
    
    protected event Action _updateEvent;
    protected event Action _onDestroy;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    
    public bool IsSelected { get; private set; }
    public event Action OnSelect;
    public event Action OnDeselect;

    public AffiliationEnum Affiliation => affiliationEnum;

    protected void Awake()
    {
        OnAwake();
    }

    protected void Start() => OnStart();
    protected void Update() => _updateEvent?.Invoke();

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }

    public virtual void CalculateCost() { }

    public virtual void TakeDamage(IDamageApplicator damageApplicator)
    {
        _healthStorage.ChangeValue(-damageApplicator.Damage);
        if (_healthStorage.CurrentValue <= 0)
            Destroy(gameObject);
    }

    public virtual void TakeRepair(IRepairApplicator repairApplicator)
    {
        _healthStorage.ChangeValue(repairApplicator.Rapair);
    }

    public void Select()
    {
        if(IsSelected) return;

        IsSelected = true;
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        if(!IsSelected) return;

        IsSelected = false;
        OnDeselect?.Invoke();
    }
    
    private void OnDestroy()
    {
        _onDestroy?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}