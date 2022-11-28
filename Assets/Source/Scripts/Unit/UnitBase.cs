using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable
{
    protected ResourceStorage _healthStorage = new ResourceStorage(100, 100);
    public float MaxHealPoints => _healthStorage.Capacity;
    public float CurrentHealPoints => _healthStorage.CurrentValue;
    protected List<AbilityBase> _abilites = new List<AbilityBase>();
    public List<AbilityBase> Abilites => _abilites;

    protected EntityStateMachine _stateMachine;

    public EntityStateMachine StateMachine => _stateMachine;

    public bool IsDied => _healthStorage.CurrentValue < 1f;

    public abstract UnitType UnitType { get; }

    public event Action<UnitBase> OnUnitDied;

    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        if (IsDied)
            return;

        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamaged();

        if (IsDied)
            OnUnitDied?.Invoke(this);
    }

    protected virtual void OnDamaged() { }
}
