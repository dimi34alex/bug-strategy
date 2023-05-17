using System;
using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable, IMiniMapShows, ITriggerable, IUnitTarget
{
    public abstract ConstructionID ConstructionID { get; }
    public UnitTargetType TargetType => UnitTargetType.Construction;
    public MiniMapID MiniMapId => MiniMapID.PlayerBuilding;
    
    public Transform Transform => transform;

    protected ResourceStorage HealPoints;
    public float MaxHealPoints => HealPoints.Capacity;
    public float CurrentHealPoints => HealPoints.CurrentValue;
    
    protected event Action _updateEvent;
    protected event Action _onDestroy;
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;


    protected void Awake()
    {
        OnAwake();
    }

    protected void Start() => OnStart();
    protected void Update() => _updateEvent?.Invoke();

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    
    public virtual void TakeDamage(IDamageApplicator damageApplicator)
    {
        HealPoints.ChangeValue(-damageApplicator.Damage);
        if (CurrentHealPoints <= 0)
            Destroy(gameObject);
    }

    public virtual void TakeRepair(IRepairApplicator repairApplicator)
    {
        HealPoints.ChangeValue(repairApplicator.Rapair);
    }

    private void OnDestroy()
    {
        _onDestroy?.Invoke();
        OnDestroyEvent?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke(this);
    }
}
