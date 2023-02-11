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
    private float _numberTownHall = 0;
    private UnitPool pool;
    private GameObject currentWorker;
    protected override void OnInit()
    {
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();
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
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100F, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("Building"))
                {
                    if (hit.transform.gameObject.GetComponent<ConstructionBase?>())
                    {
                        ConstructionID constructionID = hit.transform.gameObject.GetComponent<ConstructionBase>().ConstructionID;
                        UI._SetBuilding(hit.transform.gameObject, constructionID);
                    }
                    else
                    {
                        throw new Exception("Error: gameobject with tag Building dont have script ConstructionBase");
                    }
                }
                else if (!MousOverUI())
                {
                    UI._SetWindow("UI_GameplayMain");
                }
            }
        }
    }

    private bool MousOverUI()//проверка что курсор игрока не наведен на UI/UX
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void _MoveBuilding(GameObject _currentBuilding)//перемещение здания по карте
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!MousOverUI() && Physics.Raycast(ray, out hit, 100F, layerMask))//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
        {
            _currentBuilding.transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

            if (Input.GetButtonDown("Fire1"))//подтверждение строительства здания
            {
                foreach (GameObject unit in pool.units)
                {
                    if (unit.GetComponent<MovingUnit>().isSelected == true && unit.gameObject.tag == "Worker")
                    {
                        unit.GetComponent<MovingUnit>().SetDestination(hit.point);
                        unit.GetComponent<WorkerDuty>().isFindingBuild = true;

                        if (currentBuildingNumber == 0)
                            _SpawnTownHall(unit);
                        if (currentBuildingNumber == 1)
                            _SpawnBarrack(unit);
                        Destroy(_currentBuilding);
                        spawnBuilding = false;
                        break;
                    }
                }
            }
            else if (Input.GetButtonDown("Fire2"))//отмена начала строительства
            {
                Destroy(_currentBuilding);
                spawnBuilding = false;
            }
        }
    }

    private void _SpawnTownHall(GameObject unit)
    {
        if (_numberTownHall < 1)
        {
            _numberTownHall++;
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
               
                progressConstruction.StartBuilding(4, ConstructionID.Town_Hall, unit);
            }
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

    private void _SpawnBarrack(GameObject unit)
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

            progressConstruction.StartBuilding(4, ConstructionID.Barrack, unit);
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
