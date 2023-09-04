using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, IUnitTarget, IMiniMapShows
{
    [SerializeField] private UnitVisibleZone _unitVisibleZone;

    public abstract MiniMapID MiniMapId { get; }

    protected ResourceStorage _healthStorage = new ResourceStorage(100, 100);
    public float MaxHealPoints => _healthStorage.Capacity;
    public float CurrentHealPoints => _healthStorage.CurrentValue;
    protected List<AbilityBase> _abilites = new List<AbilityBase>();
    public List<AbilityBase> Abilites => _abilites;

 
    protected EntityStateMachine _stateMachine;

    public EntityStateMachine StateMachine => _stateMachine;

    public bool IsDied => _healthStorage.CurrentValue < 1f;
    public UnitPathData CurrentPathData { get; private set; }
    public UnitVisibleZone VisibleZone => _unitVisibleZone;

    public abstract UnitType UnitType { get; }

    public Transform Transform => transform;
    public UnitTargetType TargetType => UnitTargetType.Other_Unit;

    public event Action<UnitBase> OnUnitPathChange;
    public event Action<UnitBase> OnUnitDied;
    public event Action<ITriggerable> OnDestroyITriggerableEvent;
    public event Action<ITriggerable> OnDisableITriggerableEvent;

    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        if (IsDied)
        {
            Debug.Log("���� " + this.gameObject.name + " �������� ");
            Destroy(this.gameObject);
            return;
        }

        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamaged();

        if (IsDied)
            OnUnitDied?.Invoke(this);
    }

    public void SetPathData(UnitPathData pathData)
    {
        CurrentPathData = pathData;
        OnUnitPathChange?.Invoke(this);
    }

    protected virtual void OnDamaged() { }

    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}


public class StateBehaviorUnit
{
    private StateBehaviorUnitID _stateBehaviorUnitID;
    public StateBehaviorUnitID StateBehaviorUnitID => _stateBehaviorUnitID;

    public StateBehaviorUnit()
    {
        _stateBehaviorUnitID = StateBehaviorUnitID.Attack;
    }

    public void SetID(StateBehaviorUnitID id)
    {
        _stateBehaviorUnitID = id;
    }
}
