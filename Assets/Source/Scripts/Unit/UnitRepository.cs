using System;
using System.Collections.Generic;

public interface IUnitRepository
{
    public event Action<UnitBase> OnUnitAdd;
    public event Action<UnitBase> OnUnitRemove;

    public void AddUnit(UnitBase unit);
    public TUnit TryGetUnit<TUnit>(UnitType unitType, Predicate<TUnit> predicate = null, bool remove = false) where TUnit : UnitBase;
}

public class UnitRepository : IUnitRepository
{
    private readonly Dictionary<UnitType, List<UnitBase>> _units;

    public event Action<UnitBase> OnUnitAdd;
    public event Action<UnitBase> OnUnitRemove;

    public UnitRepository()
    {
        _units = new Dictionary<UnitType, List<UnitBase>>(5);
    }

    public void AddUnit(UnitBase unit)
    {
        if (!_units.ContainsKey(unit.UnitType))
            _units.Add(unit.UnitType, new List<UnitBase>(5));

        _units[unit.UnitType].Add(unit);
    }

    public TUnit TryGetUnit<TUnit>(UnitType unitType, Predicate<TUnit> predicate = null, bool remove = false) where TUnit : UnitBase
    {
        if (!_units.TryGetValue(unitType, out List<UnitBase> units))
            return default;

        int index = units.IndexOf(unit => unit.CastPossible<TUnit>() && (predicate is null || predicate(unit.Cast<TUnit>())));

        if (index is -1)
            return default;

        TUnit unit = units[index].Cast<TUnit>();

        if (remove)
            units.RemoveAt(index);

        return unit;
    }
}

