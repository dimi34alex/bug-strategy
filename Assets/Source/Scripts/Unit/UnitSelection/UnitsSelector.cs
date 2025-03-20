using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitsSelector
    {
        private readonly IUnitRepository _unitRepository;
        private readonly UnitsSelectorRepository _unitsSelectorRepository;
        private readonly UnitsSelectorRepository _enemyUnitsSelector;

        private List<UnitBase> AllUnits => _unitRepository.AllUnits;

        public event Action OnSelectionChanged;

        public UnitsSelector(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;

            _unitsSelectorRepository = new UnitsSelectorRepository();
            _enemyUnitsSelector = new UnitsSelectorRepository();
        }

        /// <returns>
        ///     return true if one or more player unit was selected, else false
        /// </returns>
        public bool SelectUnits(Vector3 selectedStartPoint, Vector3 selectedEndPoint, AffiliationEnum playerAffiliation)
        {
            var startPoint = new Vector2(selectedStartPoint.x, selectedStartPoint.z);
            var endPoint = new Vector2(selectedEndPoint.x, selectedEndPoint.z);

            if ((startPoint - endPoint).magnitude < 1)
            {
                var offsetSelection = new Vector2(1, 1);
                var oldStartPoint = startPoint;
                startPoint = oldStartPoint - offsetSelection;
                endPoint = oldStartPoint + offsetSelection;
            }

            var anyPlayerUnit = false;
            foreach (var unit in AllUnits)
            {
                var unitCoordinate = new Vector2(unit.transform.position.x, unit.transform.position.z);
                if (CheckSelectionBounds(unitCoordinate, startPoint, endPoint))
                {
                    if (unit.Affiliation == playerAffiliation)
                    {
                        anyPlayerUnit = true;
                        _unitsSelectorRepository.SelectUnit(unit);
                        unit.Select(true);
                    }
                    else
                    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                        anyPlayerUnit = true;
                        _unitsSelectorRepository.SelectUnit(unit);
#endif
                        _enemyUnitsSelector.SelectUnit(unit);
                        unit.Select(false);
                    }
                }
            }

            OnSelectionChanged?.Invoke();

            return anyPlayerUnit;
        }

        private static bool CheckSelectionBounds(Vector2 point, Vector2 selectionStartPoint, Vector2 selectionEndPoint)
        {
            var minX = Mathf.Min(selectionStartPoint.x, selectionEndPoint.x);
            var maxX = Mathf.Max(selectionStartPoint.x, selectionEndPoint.x);

            var minY = Mathf.Min(selectionStartPoint.y, selectionEndPoint.y);
            var maxY = Mathf.Max(selectionStartPoint.y, selectionEndPoint.y);

            return CheckValueInRange(point.x, minX, maxX) && CheckValueInRange(point.y, minY, maxY);
        }

        private static bool CheckValueInRange(float value, float minValue, float maxValue)
            => (value > minValue && value < maxValue);

        public IReadOnlyList<UnitBase> GetPlayerSelectedUnits()
            => _unitsSelectorRepository.GetSelectedUnits();

        public IReadOnlyList<UnitBase> GetEnemySelectedUnits()
            => _enemyUnitsSelector.GetSelectedUnits();
        
        public IReadOnlyList<UnitBase> GetPlayerSelectedUnits(UnitType unitType)
            => _unitsSelectorRepository.GetSelectedUnits(unitType);

        public IReadOnlyList<UnitBase> GetEnemySelectedUnits(UnitType unitType)
            => _enemyUnitsSelector.GetSelectedUnits(unitType);
        
        public void DeselectAll()
        {
            _unitsSelectorRepository.DeselectAll();
            _enemyUnitsSelector.DeselectAll();
            OnSelectionChanged?.Invoke();
        }
    }
}