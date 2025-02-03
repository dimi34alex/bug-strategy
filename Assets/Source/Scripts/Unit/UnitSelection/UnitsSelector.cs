using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitsSelector
    {
        [Inject] private readonly IUnitRepository _unitRepository;

        private List<UnitBase> UnitsInScreen => _unitRepository.AllUnits;
        private readonly List<UnitBase> _selectedUnits = new();

        public event Action OnSelectionChanged;

        public bool SelectUnits(Vector3 selectedStartPoint, Vector3 selectedEndPoint)
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

            var anyUnit = false;
            foreach (var unit in UnitsInScreen)
            {
                var unitCoordinate = new Vector2(unit.transform.position.x, unit.transform.position.z);

                if (CheckSelectionBounds(unitCoordinate, startPoint, endPoint))
                {
                    anyUnit = true;
                    unit.Select();
                    unit.OnUnitDeactivation += Deselect;
                    _selectedUnits.Add(unit);
                }
            }

            OnSelectionChanged?.Invoke();

            return anyUnit;
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

    public static class RingStepPositionGenerator
    {
        private static float _ringStep = 2;
        private static int _unitsCountRingStep = 8;

        public static List<Vector3> TakeRingsPositions(Vector3 center, int countPositions)
        {
            float currentDistance = _ringStep;
            int currentCount = _unitsCountRingStep;

            List<Vector3> positions = new List<Vector3>();
            positions.Add(center);
            for (int i = positions.Count; i < countPositions; i = positions.Count)
            {
                positions.AddRange(TakeRingPositions(center, currentDistance, currentCount));
                currentDistance += _ringStep;
                currentCount += _unitsCountRingStep;
            }

            return positions;
        }

        private static List<Vector3> TakeRingPositions(Vector3 center, float distanceFromCenter, int positionsCount)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < positionsCount; i++)
            {
                float angle = i * (360 / positionsCount);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * new Vector3(1, 0, 0);
                positions.Add(center + distanceFromCenter * direction);
            }

            return positions;
        }
    }
}