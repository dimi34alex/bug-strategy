using System;
using System.Collections.Generic;
using System.Linq;
using CycleFramework.Extensions;

namespace BugStrategy.Unit
{
    public interface IUnitRepository
    {
        public event Action<UnitBase> OnUnitAdd;
        public event Action<UnitBase> OnUnitRemove;
        public IReadOnlyDictionary<UnitType, List<UnitBase>> Units { get; }
        public void AddUnit(UnitBase unit);
        public TUnit TryGetUnit<TUnit>(UnitType unitType, Predicate<TUnit> predicate = null, bool remove = false) where TUnit : UnitBase;
    }

    public class UnitRepository : IUnitRepository
    {
        private readonly Dictionary<UnitType, List<UnitBase>> _units;

        public IReadOnlyDictionary<UnitType, List<UnitBase>> Units => _units;

        public List<UnitBase> AllUnits => _units.Values.SelectMany(list => list).OfType<UnitBase>().ToList();
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

            unit.ElementReturnEvent += RemoveUnit;

            _units[unit.UnitType].Add(unit);
        
            OnUnitAdd?.Invoke(unit);
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
            {
                units.RemoveAt(index);
                OnUnitRemove?.Invoke(unit);
            }

            return unit;
        }

        public void RemoveUnit(UnitBase unit)
        {
            if (_units.TryGetValue(unit.UnitType, out var units) && units.Remove(unit))
                OnUnitRemove?.Invoke(unit);
        }
    }
}