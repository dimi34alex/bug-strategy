using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using UnityEngine.EventSystems;

public class Test_Builder_TownHall : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;
    [SerializeField] LayerMask layerMask;
    [SerializeField] List<GameObject> buildings;//массив префабов зданий
    GameObject currentObj;
    UI_Controller UI;
    bool spawnBuilding = false;

    protected override void OnInit(){
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();

    }

    protected override void OnUpdate()
    {
        if (spawnBuilding){
            _MoveBuilding(currentObj);
        }
        else{
            _Main();
        }
    }

    private void _Main()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100F, layerMask) && Input.GetButtonDown("Fire1"))//если рэйкаст сталкиваеться с чем нибудь, проверяем это здание или нет
        {
            if (hit.transform.gameObject.tag == "Building")//если да, то вызываем через здание UX/UI меню этого здания
            {
                hit.transform.gameObject.GetComponent<TownHall>()._CallBuildingMenu("UI_TownHallMenu");
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

    void _MoveBuilding(GameObject _currentObj)//перемещение здания по карте
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100F, layerMask) && !MousOverUI())//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
        {
            _currentObj.transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

            if (Input.GetButtonDown("Fire1"))//лкм
            {
                _SpawnBuilding_Progress_Construction();
                Destroy(_currentObj);
                spawnBuilding = false;
            }
            else if (Input.GetButtonDown("Fire2"))//Если мы только что заспавнили здание и нажали пкм, то здание будет уничтожено
            {
                Destroy(_currentObj);
                spawnBuilding = false;
            }
        }
    }

    private void _SpawnBuilding_Progress_Construction()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

        if (index > -1)
        {
            Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

            if (FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(position.ToInt(), false))
                return;

            BuildingProgressConstruction progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.Building_Progress_Construction);
            progressConstruction.transform.position = position;
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position.ToInt(), progressConstruction);

            progressConstruction.OnTimerEnd += c => CreateDefaultConstruction(c, position.ToInt());
            progressConstruction.StartBuilding(4, ConstructionID.Town_Hall);
        }
    }


    private void CreateDefaultConstruction(BuildingProgressConstruction buildingProgressConstruction, Vector3Int position)
    {
        TownHall townHall = _constructionFactory.Create<TownHall>(buildingProgressConstruction.BuildingConstructionID);

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, townHall);
        townHall.transform.position = position;
    }

    public void _SpawnBuilding(int number)
    {
        spawnBuilding = true;
        currentObj = Instantiate(buildings[number]);
    }
}
