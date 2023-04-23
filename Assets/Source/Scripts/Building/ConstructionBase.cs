using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, IDamagable, IRepairable, IMiniMapShows
{
    public abstract ConstructionID ConstructionID { get; }

    public MiniMapID MiniMapId => MiniMapID.PlayerBuilding;
    public event Action<Transform> RemoveMiniMapIcon;

    protected ResourceStorage HealPoints;
    public float MaxHealPoints => HealPoints.Capacity;
    public float CurrentHealPoints => HealPoints.CurrentValue;
    
    protected event Action _updateEvent;
    protected event Action _onDestroy;

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
        {
            _onDestroy?.Invoke();
            RemoveMiniMapIcon?.Invoke(transform);
            Destroy(gameObject);
        }
    }

    public virtual void TakeRepair(IRepairApplicator repairApplicator)
    {
        HealPoints.ChangeValue(repairApplicator.Rapair);
    }

    private void OnDestroy()
    {
        RemoveMiniMapIcon?.Invoke(transform);
    }
}