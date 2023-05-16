using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
	public static UnitSelection Instance { get; private set; }

	[SerializeField] private LayerMask unitAndGroundLayers;
	[SerializeField] private GameObject targetPositionMarkerPrefab;

	[SerializeField] private float ringStep;
	[SerializeField] private int unitsCounRingStep;
	
	public List<Vector3> positions;
    public Texture texture;

    private Ray ray;
    private RaycastHit hit;
    private Vector3 mouseStartPoint;
	private float mouseX;
	private float mouseY;
	private float selectedHeight;
	private float selectedWidth;
	private Vector3 selectedStartPoint;
	private Vector3 selectedEndPoint;
    private bool isSelecting;
	private bool workerSelected;
	private bool anySelected;
	private UnitPool pool;

	private List<MovingUnit> _selectedUnits = new List<MovingUnit>();
	private Pool<UnitsTargetPositionMarker> _markersPool;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}

		Instance = this;
		
		_markersPool = new Pool<UnitsTargetPositionMarker>(AddTargetPositionMarker, 3, false);
	}

	void Start()
    {
        pool = GetComponent<UnitPool>();
    }

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isSelecting = true;
			mouseStartPoint = Input.mousePosition;

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 50f, unitAndGroundLayers, QueryTriggerInteraction.Ignore))
			{
				selectedStartPoint = hit.point;
			}
		}

		mouseX = Input.mousePosition.x;
		mouseY = Screen.height - Input.mousePosition.y;
		selectedWidth = mouseStartPoint.x - mouseX;
		selectedHeight = Input.mousePosition.y - mouseStartPoint.y;

		if (Input.GetMouseButtonUp(0))
		{
			isSelecting = false;
			DeselectAll();

			workerSelected = false;
			anySelected = false;

			if (mouseStartPoint == Input.mousePosition)
			{
				SelectOne();
			}

			else
			{
				SelectMany();
			}

			pool.SelectionCheck();
		}
		
		if (Input.GetMouseButtonDown(1) && _selectedUnits.Count > 0 && !isSelecting)
		{
			Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit newHit;
			
			if (Physics.Raycast(newRay, out newHit, 50f, unitAndGroundLayers, QueryTriggerInteraction.Ignore))
			{
				string tag = newHit.collider.tag;
				Vector3 targetPosition = newHit.point;

				positions = TakeRingsPositions(targetPosition, ringStep, unitsCounRingStep, _selectedUnits.Count);

				int n = 0;
				foreach (var unit in _selectedUnits)
				{
					unit.GiveOrder(tag, positions[n++]);
				}

				_markersPool.ExtractElement().SetPosition(targetPosition);
			}
		}
	}
	
	private List<Vector3> TakeRingsPositions(Vector3 center, float ringStep, int unitsCountRingStep, int fukkUnitsCount)
	{
		float currentDistance = ringStep;
		int currentCount = unitsCountRingStep;
		
		List<Vector3> positions = new List<Vector3>();
		positions.Add(center);
		for (int i = positions.Count; i < fukkUnitsCount; i = positions.Count)
		{
			positions.AddRange(TakeRingPositions(center, currentDistance, currentCount));
			currentDistance += ringStep;
			currentCount += unitsCountRingStep;
		}

		return positions;
	}
    
	private List<Vector3> TakeRingPositions(Vector3 center, float distanceFromCenter, int unitsCount)
	{
		List<Vector3> positions = new List<Vector3>();
		for (int i = 0; i < unitsCount; i++)
		{
			float angle = i * (360 / unitsCount);
			Vector3 direction = Quaternion.Euler(0, angle, 0) * new Vector3(1, 0,0);
			positions.Add(center + distanceFromCenter * direction);
		}

		return positions;
	}
	
	
	
	
	private void SelectOne()
	{
		if (hit.collider == null)
			return;
		if (hit.collider.gameObject.CompareTag("Unit"))
		{
			MovingUnit movingUnit = hit.collider.gameObject.GetComponent<MovingUnit>();
			movingUnit.isSelected = true;
			_selectedUnits.Add(movingUnit);
			
			anySelected = true;
		}
		else if (hit.collider.gameObject.CompareTag("Worker"))
		{
			MovingUnit movingUnit = hit.collider.gameObject.GetComponent<MovingUnit>();
			movingUnit.isSelected = true;
			_selectedUnits.Add(movingUnit);
			
			anySelected = true;
			workerSelected = true;
		}

		UI_Call();
	}

	private void SelectMany()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 50f, unitAndGroundLayers, QueryTriggerInteraction.Ignore))
		{
			selectedEndPoint = hit.point;
		}

		SelectHighlighted();
	}

    private void SelectHighlighted()
    {
        foreach (MovingUnit unit in pool.movingUnits)
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if ((x > selectedStartPoint.x && x < selectedEndPoint.x) || (x < selectedStartPoint.x && x > selectedEndPoint.x))
            {
                if ((z > selectedStartPoint.z && z < selectedEndPoint.z) || (z < selectedStartPoint.z && z > selectedEndPoint.z))
                {
	                MovingUnit movingUnit = unit;
	                movingUnit.isSelected = true;
	                _selectedUnits.Add(movingUnit);
	                anySelected = true;

	                if (unit.gameObject.CompareTag("Worker"))
	                {
		                workerSelected = true;
	                }
                }
            }
        }

		UI_Call();
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            GUI.DrawTexture(new Rect(mouseX, mouseY, selectedWidth, selectedHeight), texture);
        }
    }

	void UI_Call()
	{
		if (workerSelected)
		{
			UI_Controller._SetWindow("UI_Buildings");
		}
		else if (anySelected)
		{
			UI_Controller._SetWindow("UI_Tactics");
		}
	}
	
	public void DeselectAll()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			foreach (MovingUnit unit in _selectedUnits)
			{
				unit.isSelected = false;
			}
			_selectedUnits.Clear();
		}
	}

	private UnitsTargetPositionMarker AddTargetPositionMarker()
	{
		GameObject targetPositionMarker = Instantiate(targetPositionMarkerPrefab, transform);
		
		return targetPositionMarker.GetComponent<UnitsTargetPositionMarker?>();
	}
}
