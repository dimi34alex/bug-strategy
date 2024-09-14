using System.Collections.Generic;
using BugStrategy.Missions;
using BugStrategy.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelection : MonoBehaviour
    {
        [Inject] private readonly UIController _uiController;
        [Inject] private readonly MissionData _missionData;
    
        private bool _isSelecting;

        private Vector3 _mouseStartSelectionPoint;
        private Vector3 _mouseEndSelectionPoint;

        private List<UnitBase> UnitsInScreen => _missionData.UnitRepository.AllUnits;
        private readonly List<UnitBase> _selectedUnits = new List<UnitBase>();

        public static Camera Camera => Camera.main;

        public static UnitSelection Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            SelectProcess();
            OrderProcess();
        }

        private void SelectProcess()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _isSelecting = true;
                _mouseStartSelectionPoint = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) && !_missionData.ConstructionSelector.TrySelect(ray))
            {
                _isSelecting = false;
                _mouseEndSelectionPoint = Input.mousePosition;

                DeselectAll();

                SelectUnits(Camera.ScreenToWorldPoint(_mouseStartSelectionPoint),
                    Camera.ScreenToWorldPoint(_mouseEndSelectionPoint));
            }
        }

        private void OrderProcess()
        {
            if (Input.GetMouseButtonDown(1) && _selectedUnits.Count > 0 && !_isSelecting)
            {
                Vector3 targetPosition = Input.mousePosition;

                targetPosition = Camera.ScreenToWorldPoint(targetPosition);

                List<Vector3> newUnitpositions =
                    RingStepPositionGenerator.TakeRingsPositions(targetPosition, _selectedUnits.Count);

                int n = 0;
                foreach (var unit in _selectedUnits)
                {
                    unit.AutoGiveOrder(null, newUnitpositions[n]);
                }

                UnitsTargetPositionMarkerFactory.Instance.Create(targetPosition);
            }
        }

        private void SelectUnits(Vector3 selectedStartPoint, Vector3 selectedEndPoint)
        {
            Vector2 startPoint = new Vector2(selectedStartPoint.x, selectedStartPoint.z);
            Vector2 endPoint = new Vector2(selectedEndPoint.x, selectedEndPoint.z);

            if ((startPoint - endPoint).magnitude < 1)
            {
                Vector2 offsetSelection = new Vector2(1, 1);
                Vector2 oldStartPoint = startPoint;
                startPoint = oldStartPoint - offsetSelection;
                endPoint = oldStartPoint + offsetSelection;
            }

            foreach (UnitBase unit in UnitsInScreen)
            {
                Vector2 unitCoordinate = new Vector2(unit.transform.position.x, unit.transform.position.z);
          
                if (CheckSelectionBounds(unitCoordinate, startPoint, endPoint))
                {
                    unit.Select();
                    _selectedUnits.Add(unit);
                }
            }

            UICall();
        }

        private bool CheckSelectionBounds(Vector2 point, Vector2 selectionStartPoint, Vector2 selectionEndPoint)
        {
            float minX = Mathf.Min(selectionStartPoint.x, selectionEndPoint.x);
            float maxX = Mathf.Max(selectionStartPoint.x, selectionEndPoint.x);

            float minY = Mathf.Min(selectionStartPoint.y, selectionEndPoint.y);
            float maxY = Mathf.Max(selectionStartPoint.y, selectionEndPoint.y);

            return CheckValueInRange(point.x, minX, maxX) && CheckValueInRange(point.y, minY, maxY);
        }

        private bool CheckValueInRange(float value, float minValue, float maxValue)
        {
            return (value > minValue && value < maxValue);
        }

        private void UICall()
        {
            if (_selectedUnits != null && _selectedUnits.Count != 0)
            {
                _uiController.SetScreen(_selectedUnits[0]);
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                    _uiController.SetScreen(UIScreenType.Gameplay);
            }
        }
        public void DeselectAllWithoutCheck()
        {
            foreach (UnitBase unit in _selectedUnits)
            {
                unit.Deselect();
            }
            _selectedUnits.Clear();
        }

        public void DeselectAll()
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // тут надо прикрутить проверку на контрл, или как там вообще ..
            {
                foreach (UnitBase unit in _selectedUnits)
                {
                    unit.Deselect();
                }
                _selectedUnits.Clear();
            }
        }
    }


    public class RingStepPositionGenerator
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