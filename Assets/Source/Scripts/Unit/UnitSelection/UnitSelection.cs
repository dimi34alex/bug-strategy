using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
    private bool _isSelecting;

    private Vector3 _mouseStartSelectionPoint;
    private Vector3 _mouseEndSelectionPoint;

    private List<MovingUnit> _unitsInScreen =>FrameworkCommander.GlobalData.UnitRepository.MovingUnits;
    private List<MovingUnit> _selectedUnits = new List<MovingUnit>();

    public Camera Camera => Camera.main;

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
        if (Input.GetMouseButtonDown(0))
        {
            _isSelecting = true;
            _mouseStartSelectionPoint = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
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

            List<Vector3> newUnitpositions =
                RingStepPositionGenerator.TakeRingsPositions(targetPosition, _selectedUnits.Count);

            int n = 0;
            foreach (var unit in _selectedUnits)
            {
         //       unit.GiveOrder(null, newUnitpositions[n++]);
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

        foreach (MovingUnit unit in _unitsInScreen)
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
        // Нужно поговорить, о том что это система в принципе плоха и ее надо переделать......
        // Определение рабочих или других классов,
        // у нас совершенно не готова система UI впринципе, из-за появления новых классов

        /*  if (unit.gameObject.CompareTag("Worker"))
          {
              workerSelected = true;
          }*/
        //Вот так не пойдет ...


        /* if (workerSelected)
         {
             UI_Controller._SetWindow("UI_Buildings");
         }
         else if (anySelected)
         {
             UI_Controller._SetWindow("UI_Tactics");
         }*/
    }


    public void DeselectAll()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // тут надо прикрутить проверку на контрл, или как там вообще ..
        {
            foreach (MovingUnit unit in _selectedUnits)
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
