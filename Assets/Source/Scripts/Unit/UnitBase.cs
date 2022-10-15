using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable
{
    protected ResourceStorage _healthStorage = new ResourceStorage(100, 100);
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
