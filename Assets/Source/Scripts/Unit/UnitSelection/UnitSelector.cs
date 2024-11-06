using System.Collections.Generic;
using BugStrategy.CustomInput;
using BugStrategy.Missions;
using BugStrategy.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask targetsLayers;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly UIController _uiController;
        [Inject] private readonly MissionData _missionData;
    
        private bool _isSelecting;

        private Vector3 _mouseStartSelectionPoint;
        private Vector3 _mouseEndSelectionPoint;

        private List<UnitBase> UnitsInScreen => _missionData.UnitRepository.AllUnits;
        private readonly List<UnitBase> _selectedUnits = new List<UnitBase>();

        public static Camera Camera => Camera.main;

        public static UnitSelector Instance { get; private set; }

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
            Ray ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);

            if (_inputProvider.LmbDown)
            {
                _isSelecting = true;
                _mouseStartSelectionPoint = _inputProvider.MousePosition;
            }

            if (_inputProvider.LmbUp && !_missionData.ConstructionSelector.TrySelect(ray))
            {
                _isSelecting = false;
                _mouseEndSelectionPoint = _inputProvider.MousePosition;

                DeselectAll();

                SelectUnits(Camera.ScreenToWorldPoint(_mouseStartSelectionPoint),
                    Camera.ScreenToWorldPoint(_mouseEndSelectionPoint));
            }
        }

        private void OrderProcess()
        {
            if (_inputProvider.RmbDown && _selectedUnits.Count > 0 && !_isSelecting)
            {
                var ray = Camera.ScreenPointToRay(_inputProvider.MousePosition);
                if (Physics.Raycast(ray, out var hit, 100F, targetsLayers, QueryTriggerInteraction.Ignore) 
                    && hit.collider.TryGetComponent(out ITarget target))
                {
                    for (int i = 0; i < _selectedUnits.Count; i++)
                        _selectedUnits[i].AutoGiveOrder(target);
                }
                else
                {
                    Vector3 targetPosition = _inputProvider.MousePosition;
                    targetPosition = Camera.ScreenToWorldPoint(targetPosition);

                    var unitPositions =
                        RingStepPositionGenerator.TakeRingsPositions(targetPosition, _selectedUnits.Count);

                    for (int i = 0; i < _selectedUnits.Count; i++)
                        _selectedUnits[i].AutoGiveOrder(null, unitPositions[i]);

                    UnitsTargetPositionMarkerFactory.Instance.Create(targetPosition);
                }
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
                if(!_uiController.IsConstructionInfoScreenActive())
                _uiController.SetScreen(_selectedUnits[0]);
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                    _uiController.SetScreen(UIScreenType.Gameplay);
            }
        }

        public IReadOnlyList<UnitBase> GetSelectedUnits(UnitType unitType) 
            => _selectedUnits.FindAll(unitBase => unitBase.UnitType == unitType);

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