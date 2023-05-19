using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using UnityEngine.EventSystems;


[Serializable]
struct BuildingDictionaryData
{
    public ConstructionID constructionID;
    [Tooltip("Movable prefab(/Prefabs/Constructions/MovableModel), NOT MAIN PREFAB")]
    public GameObject constructionModel;
}
public class Test_Builder_TownHall : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;
    [SerializeField] LayerMask layerMask;
    
    [SerializeField] private List<BuildingDictionaryData> buildingsDictionaryData;
    [SerializeField] Dictionary<ConstructionID, GameObject> buildings = new Dictionary<ConstructionID, GameObject>();
    GameObject currentBuilding;
    ConstructionID currentConstructionID;
    
    bool spawnBuilding = false;
    private float _numberTownHall = 0;
    private UnitPool pool;
    private GameObject currentWorker;
    protected override void OnInit()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();

        for (int n = 0; n < buildingsDictionaryData.Count; n++)
            buildings.Add(buildingsDictionaryData[n].constructionID, buildingsDictionaryData[n].constructionModel);
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
            if (Physics.Raycast(ray, out hit, 100F, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject.CompareTag("Building"))
                {
                    if (hit.transform.gameObject.GetComponent<ConstructionBase?>())
                    {
                        ConstructionID constructionID = hit.transform.gameObject.GetComponent<ConstructionBase>().ConstructionID;
                        UI_Controller._SetBuilding(hit.transform.gameObject, constructionID);
                    }
                    else
                    {
                        throw new Exception("Error: gameObject with tag Building dont have script ConstructionBase");
                    }
                }
                else if (!MousOverUI())
                {
                    UI_Controller._SetWindow("UI_GameplayMain");
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

        if (!MousOverUI() && Physics.Raycast(ray, out hit, 100F, layerMask, QueryTriggerInteraction.Ignore))//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
        {
            _currentBuilding.transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

            if (Input.GetButtonDown("Fire1"))//подтверждение строительства здания
            {
                if (hit.collider.name == "TileBase")
                {
                    if (!hit.collider.GetComponent<Tile>().Visible)
                    {
                        Destroy(_currentBuilding);
                        spawnBuilding = false;
                        return;
                    }
                }
                
                foreach (GameObject unit in pool.units)
                {
                    if (unit.GetComponent<MovingUnit>().isSelected == true && unit.gameObject.CompareTag("Worker"))
                    {
                        unit.GetComponent<MovingUnit>().SetDestination(hit.point);
                        unit.gameObject.transform.GetChild(4).GetComponent<WorkerDuty>().isFindingBuild = true;

                        if (currentConstructionID == ConstructionID.Town_Hall)
                            _SpawnTownHall(unit);
                        if (currentConstructionID == ConstructionID.Barrack)
                            _SpawnBarrack(unit);
                        if (currentConstructionID == ConstructionID.BeeHouse)
                            _SpawnBeeHouse(unit);
                        if (currentConstructionID == ConstructionID.Bees_Wax_Produce_Construction)
                            _SpawnBeesWaxProduceConstruction(unit);

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

                Affiliation unitsTeam = unit.gameObject.GetComponent<Affiliation>();

                progressConstruction.OnTimerEnd += c => CreateTownHall(c, position, unitsTeam.affiliation);
               
                progressConstruction.StartBuilding(4, ConstructionID.Town_Hall, unit);
            }
        }
    }

    private void CreateTownHall(BuildingProgressConstruction buildingProgressConstruction, Vector3 position, AffiliationEnum team)
    {
        TownHall townHall = _constructionFactory.Create<TownHall>(buildingProgressConstruction.BuildingConstructionID);

        townHall.gameObject.GetComponent<Affiliation>().affiliation = team;

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

            Affiliation unitsTeam = unit.gameObject.GetComponent<Affiliation>();

            progressConstruction.OnTimerEnd += c => CreateBarrack(c, position, unitsTeam.affiliation);

            progressConstruction.StartBuilding(4, ConstructionID.Barrack, unit);
        }
    }
    private void CreateBarrack(BuildingProgressConstruction buildingProgressConstruction, Vector3 position, AffiliationEnum team)
    {
        Barrack barrack = _constructionFactory.Create<Barrack>(buildingProgressConstruction.BuildingConstructionID);

        barrack.gameObject.GetComponent<Affiliation>().affiliation = team;

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, barrack);
        barrack.transform.position = position;
    }
    
    
    private void _SpawnBeeHouse(GameObject unit)
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

            Affiliation unitsTeam = unit.gameObject.GetComponent<Affiliation>();

            progressConstruction.OnTimerEnd += c => CreateBeeHouse(c, position, unitsTeam.affiliation);

            progressConstruction.StartBuilding(4, ConstructionID.BeeHouse, unit);
        }
    }
    private void CreateBeeHouse(BuildingProgressConstruction buildingProgressConstruction, Vector3 position, AffiliationEnum team)
    {
        BeeHouse beeHouse = _constructionFactory.Create<BeeHouse>(buildingProgressConstruction.BuildingConstructionID);

        beeHouse.gameObject.GetComponent<Affiliation>().affiliation = team;

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, beeHouse);
        beeHouse.transform.position = position;
    }
    
    
    private void _SpawnBeesWaxProduceConstruction(GameObject unit)
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

            Affiliation unitsTeam = unit.gameObject.GetComponent<Affiliation>();

            progressConstruction.OnTimerEnd += c => CreateBeesWaxProduceConstruction(c, position, unitsTeam.affiliation);

            progressConstruction.StartBuilding(4, ConstructionID.Bees_Wax_Produce_Construction, unit);
        }
    }
    private void CreateBeesWaxProduceConstruction(BuildingProgressConstruction buildingProgressConstruction, Vector3 position, AffiliationEnum team)
    {
        BeesWaxProduceConstruction beesWaxProduceConstruction = _constructionFactory.Create<BeesWaxProduceConstruction>(buildingProgressConstruction.BuildingConstructionID);

        beesWaxProduceConstruction.gameObject.GetComponent<Affiliation>().affiliation = team;

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, beesWaxProduceConstruction);
        beesWaxProduceConstruction.transform.position = position;
    }
    
    
    public void _SpawnBuilding(ConstructionID constructionID)
    {
        currentConstructionID = constructionID;
        spawnBuilding = true;
        currentBuilding = Instantiate(buildings[constructionID]);
    }
}
