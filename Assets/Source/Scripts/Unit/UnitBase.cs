using System;
using System.Collections.Generic;
using UnityEngine;
using MiniMapSystem;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, IUnitTarget, IMiniMapObject,
    SelectableSystem.ISelectable, IAffiliation, IPoolable<UnitBase, UnitType>
{
    [SerializeField] private UnitVisibleZone _unitVisibleZone;

    protected ResourceStorage _healthStorage { get; set; } = new ResourceStorage(100, 100);
    protected EntityStateMachine _stateMachine;
    protected List<AbilityBase> _abilites = new List<AbilityBase>();

    public IReadOnlyResourceStorage HealthStorage => _healthStorage;
    public IReadOnlyList<AbilityBase> Abilities => _abilites;
    public EntityStateMachine StateMachine => _stateMachine;
    public bool IsDied => _healthStorage.CurrentValue < 1f;
    public UnitPathData CurrentPathData { get; private set; }
    public UnitVisibleZone VisibleZone => _unitVisibleZone;
   
    public Transform Transform => transform;
    public UnitTargetType TargetType => UnitTargetType.Other_Unit;
    public bool IsSelected { get; private set; }
    public MiniMapObjectType MiniMapObjectType => MiniMapObjectType.Unit;

    public event Action<UnitBase> OnUnitPathChange;
    public event Action<UnitBase> OnUnitDied;
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    public event Action OnSelect;
    public event Action OnDeselect;

    public event Action<UnitBase> ElementReturnEvent;
    public event Action<UnitBase> ElementDestroyEvent;

    public abstract AffiliationEnum Affiliation { get; }

    public abstract UnitType UnitType { get; }
    public UnitType Identifier => UnitType;


    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamaged();

        if (IsDied)
        {
            Debug.Log("���� " + this.gameObject.name + " �������� ");
            OnUnitDied?.Invoke(this);
            ElementDestroyEvent?.Invoke(this);
            return;
        }
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