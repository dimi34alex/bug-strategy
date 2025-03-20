using System.Collections.Generic;
using System;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitsSelectorRepository
    {
        private readonly List<UnitBase> _selectedUnits = new();

        public event Action OnSelectionChanged;

        public void SelectUnit(UnitBase unit)
        {
            _selectedUnits.Add(unit);
            unit.OnUnitDeactivation += Deselect;
            
            OnSelectionChanged?.Invoke();
        }

        public IReadOnlyList<UnitBase> GetSelectedUnits()
            => _selectedUnits;

        public IReadOnlyList<UnitBase> GetSelectedUnits(UnitType unitType)
            => _selectedUnits.FindAll(unitBase => unitBase.UnitType == unitType);

        public void DeselectAll()
        {
            foreach (var unit in _selectedUnits)
                DeselectUnit(unit);

            _selectedUnits.Clear();

            OnSelectionChanged?.Invoke();
        }
        
        public void Deselect(UnitBase unit)
        {
            if (!_selectedUnits.Contains(unit))
                return;

            DeselectUnit(unit);
            _selectedUnits.Remove(unit);
            
            OnSelectionChanged?.Invoke();
        }
        
        private void DeselectUnit(UnitBase unit)
        {
            unit.OnUnitDeactivation -= Deselect;
            unit.Deselect();
        }
    }
}