using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Test_MouseRayCast_TownHall : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    UI_Controller UI;
    private void Awake()
    {
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100F, layerMask) && Input.GetButtonDown("Fire1"))//если рэйкаст сталкиваеться с чем нибудь, проверяем это здание или нет
        {
            if (hit.transform.gameObject.tag == "Building")//если да, то вызываем через здание UX/UI меню этого здания
            {
                hit.transform.gameObject.GetComponent<BuildingBase>()._CallBuildingMenu("UI_TownHallMenu");
                UI._SetBuilding(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.tag != "UI" && !MousOverUI())
            {
                UI._SetWindow("UI_GameplayMain");
            }
        }
    }

    bool MousOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
