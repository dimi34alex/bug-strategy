using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
	[SerializeField] private LayerMask unitAndGroundLayers;

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
			pool.DeselectAll();

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
	}

	private void SelectOne()
	{
		if (hit.collider == null)
			return;
		if (hit.collider.gameObject.tag == "Unit")
		{
			hit.collider.gameObject.GetComponent<MovingUnit>().isSelected = true;
			anySelected = true;
		}
		else if (hit.collider.gameObject.tag == "Worker")
		{
			hit.collider.gameObject.GetComponent<MovingUnit>().isSelected = true;
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
                    unit.GetComponent<MovingUnit>().isSelected = true;
					anySelected = true;

					if (unit.gameObject.tag == "Worker")
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
}
