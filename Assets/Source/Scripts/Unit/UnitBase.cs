using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, IUnitTarget, IMiniMapShows,
    SelectableSystem.ISelectable
{
    [SerializeField] private UnitVisibleZone _unitVisibleZone;

    public abstract MiniMapID MiniMapId { get; }

    protected ResourceStorage _healthStorage { get; set; } = new ResourceStorage(100, 100);
    public IReadOnlyResourceStorage HealthStorage => _healthStorage;
    protected List<AbilityBase> _abilites = new List<AbilityBase>();
    public IReadOnlyList<AbilityBase> Abilities => _abilites;

    protected EntityStateMachine _stateMachine;

    public EntityStateMachine StateMachine => _stateMachine;

    public bool IsDied => _healthStorage.CurrentValue < 1f;
    public UnitPathData CurrentPathData { get; private set; }
    public UnitVisibleZone VisibleZone => _unitVisibleZone;

    public abstract UnitType UnitType { get; }

    public Transform Transform => transform;
    public UnitTargetType TargetType => UnitTargetType.Other_Unit;
    public bool IsSelected { get; private set; }

    public event Action<UnitBase> OnUnitPathChange;
    public event Action<UnitBase> OnUnitDied;
    public event Action<ITriggerable> OnDestroyITriggerableEvent;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnSelect;
    public event Action OnDeselect;
    
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
