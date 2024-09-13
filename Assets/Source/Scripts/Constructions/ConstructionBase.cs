using System;
using UnityEngine;
using MiniMapSystem;
using Source.Scripts.Missions;
using Zenject;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable, IMiniMapObject,
    ITriggerable, IUnitTarget, SelectableSystem.ISelectable, IAffiliation
{
    [Inject] protected readonly MissionData MissionData;
    
    public AffiliationEnum Affiliation { get; private set; }
    public abstract FractionType Fraction { get; }

    protected ResourceStorage _healthStorage = new ResourceStorage(0,0);

    public bool IsSelected { get; private set; }
    public bool IsActive { get; protected set; } = true;
    public bool IsAlive => IsActive && _healthStorage.CurrentValue > 0f;
    
    public abstract ConstructionID ConstructionID { get; }
    public UnitTargetType TargetType => UnitTargetType.Construction;
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.Construction;
    public Transform Transform => transform;
    public IReadOnlyResourceStorage HealthStorage => _healthStorage;
    
    protected event Action _updateEvent;
    protected event Action _onDestroy;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnSelect;
    public event Action OnDeselect;
    public event Action<IUnitTarget> OnDeactivation;
    public event Action OnDestruction;
    public event Action Initialized;
    
    protected void Awake() => OnAwake();
    protected void Start() => OnStart();
    protected void Update() => _updateEvent?.Invoke();

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }

    public void Initialize(AffiliationEnum newAffiliation)
    {
        Affiliation = newAffiliation;
        Initialized?.Invoke();
    }
    
    public virtual void TakeDamage(IUnitTarget attacker, IDamageApplicator damageApplicator, float damageScale = 1)
    {
        if (!IsAlive)
        {
            Debug.LogError($"You try damage construction that already destructed {attacker} | {damageApplicator} | {this}");
            return;
        }

        _healthStorage.ChangeValue(-damageApplicator.Damage * damageScale);
        if (_healthStorage.CurrentValue <= 0)
        {
            IsActive = false;
            OnDeactivation?.Invoke(this);
            MissionData.ConstructionsRepository.GetConstruction(transform.position, true);
            OnDestruction?.Invoke();
            Destroy(gameObject);
        }
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

    protected void SendDeactivateEvent() 
        => OnDeactivation?.Invoke(this);
    
    private void OnDestroy()
    {
        IsActive = false;
        _onDestroy?.Invoke();
        OnDeactivation?.Invoke(this);
    }

    private void OnDisable()
    {
        IsActive = false;
        OnDisableITriggerableEvent?.Invoke(this);
    }
}