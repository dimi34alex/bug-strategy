using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
	public static UnitSelection Instance { get; private set; }

	[SerializeField] private LayerMask unitAndGroundLayers;
	[SerializeField] private List<MovingUnit> selectedUnits = new List<MovingUnit>();

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


	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}

		Instance = this;
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
		
		if (Input.GetMouseButton(1))
		{
			Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit newHit;
			
			if (Physics.Raycast(newRay, out newHit, 50f, unitAndGroundLayers, QueryTriggerInteraction.Ignore))
			{
				foreach (var unit in selectedUnits)
				{
					unit.GiveOrder(newHit);
				}
			}
		}
	}
	
	private void SelectOne()
	{
		if (hit.collider == null)
			return;
		if (hit.collider.gameObject.CompareTag("Unit"))
		{
			MovingUnit movingUnit = hit.collider.gameObject.GetComponent<MovingUnit>();
			movingUnit.isSelected = true;
			selectedUnits.Add(movingUnit);
			
			anySelected = true;
		}
		else if (hit.collider.gameObject.CompareTag("Worker"))
		{
			MovingUnit movingUnit = hit.collider.gameObject.GetComponent<MovingUnit>();
			movingUnit.isSelected = true;
			selectedUnits.Add(movingUnit);
			
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
        foreach (GameObject unit in pool.units)
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if ((x > selectedStartPoint.x && x < selectedEndPoint.x) || (x < selectedStartPoint.x && x > selectedEndPoint.x))
            {
                if ((z > selectedStartPoint.z && z < selectedEndPoint.z) || (z < selectedStartPoint.z && z > selectedEndPoint.z))
                {
	                MovingUnit movingUnit = unit.GetComponent<MovingUnit>();
	                movingUnit.isSelected = true;
	                selectedUnits.Add(movingUnit);
                    
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
			foreach (MovingUnit unit in selectedUnits)
			{
				unit.isSelected = false;
			}
		}
	}
}
