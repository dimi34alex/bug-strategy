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
    GameObject currentBuilding;
    int currentBuildingNumber;
    UI_Controller UI;
    bool spawnBuilding = false;

    protected override void OnInit()
    {
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
    }

    protected override void OnUpdate()
    {
        if (spawnBuilding)
        {
            _MoveBuilding(currentBuilding);
        }
        else
        {
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
                if (hit.transform.gameObject.GetComponent<TownHall?>())
                    hit.transform.gameObject.GetComponent<TownHall>().CallBuildingMenu("UI_TownHallMenu");
                else if (hit.transform.gameObject.GetComponent<Barrack?>())
                    hit.transform.gameObject.GetComponent<Barrack>().CallBuildingMenu("UI_BarracksMenu");
            }
            else if (hit.transform.gameObject.tag != "UI" && !MousOverUI())
            {
                UI._SetWindow("UI_GameplayMain");
            }
        }
    }

    bool MousOverUI()//проверка что курсор игрока не наведен на UI/UX
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void _MoveBuilding(GameObject _currentBuilding)//перемещение здания по карте
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100F, layerMask) && !MousOverUI())//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
        {
            _currentBuilding.transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

            if (Input.GetButtonDown("Fire1"))//подтверждение строительства здания
            {
                if (currentBuildingNumber == 0)
                    _SpawnTownHall();
                if (currentBuildingNumber == 1)
                    _SpawnBarrack();
                Destroy(_currentBuilding);
                spawnBuilding = false;
            }
            else if (Input.GetButtonDown("Fire2"))//отмена начала строительства
            {
                Destroy(_currentBuilding);
                spawnBuilding = false;
            }
        }
    }

    private void _SpawnTownHall()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

        if (index > -1)
        {
            Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

            if (FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(position, false))
            {
                Debug.Log("Invalid place");
                return;
            }
            BuildingProgressConstruction progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.Building_Progress_Construction);
            progressConstruction.transform.position = position;
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, progressConstruction);

            progressConstruction.OnTimerEnd += c => CreateTownHall(c, position);
            progressConstruction.StartBuilding(4, ConstructionID.Town_Hall);
        }
    }
    private void CreateTownHall(BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
    {
        TownHall townHall = _constructionFactory.Create<TownHall>(buildingProgressConstruction.BuildingConstructionID);

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, townHall);
        townHall.transform.position = position;
    }

    private void _SpawnBarrack()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

        if (index > -1)
        {
            Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

            if (FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(position, false))
            {
                Debug.Log("Invalid place");
                return;
            }
            BuildingProgressConstruction progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.Building_Progress_Construction);
            progressConstruction.transform.position = position;
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, progressConstruction);

            progressConstruction.OnTimerEnd += c => CreateBarrack(c, position);
            progressConstruction.StartBuilding(4, ConstructionID.Barrack);
        }
    }
    private void CreateBarrack(BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
    {
        Barrack barrack = _constructionFactory.Create<Barrack>(buildingProgressConstruction.BuildingConstructionID);

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, barrack);
        barrack.transform.position = position;
    }

    public void _SpawnBuilding(int number)
    {
        currentBuildingNumber = number;
        spawnBuilding = true;
        currentBuilding = Instantiate(buildings[number]);
    }
}
