using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisibleZone : TriggerZone
{
    [SerializeField] private List<IUnitTarget> _targets = new List<IUnitTarget>();
    private Func<IUnitTarget, bool> _filter = t => true;

    public new IReadOnlyList<IUnitTarget> ContainsComponents => _targets;

    public new event Action<IUnitTarget> EnterEvent;
    public new event Action<IUnitTarget> ExitEvent;

    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is IUnitTarget && _filter(t.Cast<IUnitTarget>());

    public bool Contains(IUnitTarget target) => _targets.Contains(target);
    public bool Contains(Predicate<IUnitTarget> predicate) => _targets.Exists(predicate);

    public void SetFilter(Func<IUnitTarget, bool> filter)
    {
        _filter = filter;
    }

    protected override void OnEnter(ITriggerable component)
    {
        IUnitTarget unitTarget = component.Cast<IUnitTarget>();

        _targets.Add(unitTarget);
        EnterEvent?.Invoke(unitTarget);
    }

    protected override void OnExit(ITriggerable component)
    {
        IUnitTarget unitTarget = component.Cast<IUnitTarget>();

        _targets.Remove(unitTarget);
        ExitEvent?.Invoke(unitTarget);
    }
}