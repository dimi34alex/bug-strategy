using System.Collections.Generic;
using BugStrategy.Unit;
using BugStrategy.Unit.Factory;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.UnitsHideCore
{
    public class UnitsHider : IHider
    {
        private readonly IAffiliation _affiliation;
        private readonly IReadOnlyList<UnitType> _access;
        private readonly UnitFactory _unitFactory;
        private readonly Transform _extractTransform;
        private readonly List<HiderCellBase> _hiderCells;
        private int _capacity;

        public IReadOnlyList<UnitType> Access => _access;
        public IReadOnlyList<HiderCellBase> HiderCells => _hiderCells;

        public UnitsHider(IAffiliation affiliation, int startCapacity, UnitFactory unitFactory, Transform extractTransform, IReadOnlyList<UnitType> access)
        {
            _affiliation = affiliation;
            _capacity = startCapacity;
            _unitFactory = unitFactory;
            _extractTransform = extractTransform;
            _hiderCells = new List<HiderCellBase>(_capacity);
            _access = access;
        }

        public void SetCapacity(int newCapacity)
        {
            _capacity = newCapacity;

            if (_hiderCells.Count >= _capacity)
            {
                Debug.LogWarning($"count of cells more then capacity");
                for (int i = _hiderCells.Count - 1; i >= _capacity; i--)
                    ExtractUnit(i);
            }
        }

        public bool CheckAccess(UnitType unitType)
            => _access.Contains(c => c == unitType);
        
        public bool HaveFreePlace()
            => _hiderCells.Count < _capacity;

        public bool TryHideUnit(IHidableUnit unit)
        {
            if (!HaveFreePlace())
            {
                Debug.LogWarning("Hider dont have free place");
                return false;
            }
            
            var cell = unit.TakeHideCell();

            if (!CheckAccess(cell.UnitType))
            {
                Debug.LogWarning("You try hide unaccessible unit");
                return false;
            }
            
            _hiderCells.Add(cell);
            return true;
        }

        public UnitBase ExtractUnit(int index)
            => ExtractUnit(index, _extractTransform.position);
        
        public UnitBase ExtractUnit(int index, Vector3 extractPosition)
        {
            if (index >= _hiderCells.Count)
            {
                Debug.LogWarning($"Invalid index");
                return null;
            }
            
            var cell = _hiderCells[index];
            _hiderCells.RemoveAt(index);
            var unit = _unitFactory.Create(cell.UnitType, extractPosition, _affiliation.Affiliation);

            if (unit.TryCast(out IHidableUnit hidableUnit))
                hidableUnit.LoadHideCell(cell);
            else
                Debug.LogError($"Unit cant be casted to IHidableUnit: {unit} {cell.UnitType}");

            return unit;
        }

        public UnitBase ExtractUpgradeUnit (int index, Vector3 extractPosition)
        {
            if(index >= _hiderCells.Count)
            {
                Debug.LogWarning($"Invalid index");
                return null;
            }

            var cell = _hiderCells[index];
            _hiderCells.RemoveAt(index);

            UnitType newUnitType;

            switch(cell.UnitType)
            {
                case UnitType.Caterpillar:
                    newUnitType = UnitType.CaterpillarLevel2;
                    break;
                default:
                    newUnitType = cell.UnitType;
                    break;
            }

            var unit = _unitFactory.Create(newUnitType, extractPosition, _affiliation.Affiliation);

            if(unit.TryCast(out IHidableUnit hidableUnit))
                hidableUnit.LoadHideCell(cell);
            else
                Debug.LogError($"Unit cant be casted to IHidableUnit: {unit} {cell.UnitType}");

            return unit;
        }
    }
}