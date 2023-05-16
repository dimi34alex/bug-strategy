using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using UnityEngine.EventSystems;
using System.Linq;

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
    [SerializeField] private Dictionary<ConstructionID, GameObject> _movableBuildingsWithID;
    [SerializeField] private ConstructionBase[] _buildings;
    private Dictionary<ConstructionID, ConstructionBase> _buildingsWithID;

    public Dictionary<ConstructionID, ConstructionBase> BuildingsWithID => _buildingsWithID;

    GameObject currentBuilding;
    ConstructionID currentConstructionID;
    
    bool spawnBuilding = false;
    private float _numberTownHall = 0;
    private UnitPool pool;
    private GameObject currentWorker;
    protected override void OnInit()
    {
        _movableBuildingsWithID = new Dictionary<ConstructionID, GameObject>();
        _buildingsWithID = _buildings.ToDictionary(x => x.ConstructionID, x => x);

        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();

        for (int n = 0; n < buildingsDictionaryData.Count; n++)
            _movableBuildingsWithID.Add(buildingsDictionaryData[n].constructionID, buildingsDictionaryData[n].constructionModel);
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
                
                foreach (MovingUnit unit in pool.movingUnits)
                {
                    if (unit.isSelected == true && unit.gameObject.CompareTag("Worker"))
                    {
                        unit.SetDestination(hit.point);
                        unit.GetComponent<WorkerDuty>().isFindingBuild = true;

                        Spawn(unit, currentConstructionID);

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

    private void Spawn(MovingUnit unit, ConstructionID id)
    {
        if (id!= ConstructionID.Town_Hall || (id == ConstructionID.Town_Hall && _numberTownHall < 1))
        {
            if (id == ConstructionID.Town_Hall)
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

                progressConstruction.OnTimerEnd += c => CreateConstruction(c, position);

                progressConstruction.StartBuilding(4, id, unit);
            }
        }
    }

    private void CreateConstruction(BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
    {
        ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(buildingProgressConstruction.BuildingConstructionID);

        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, construction);
        construction.transform.position = position;
    }

    public void SpawnMovableBuilding(ConstructionID constructionID)
    {
        currentConstructionID = constructionID;
        spawnBuilding = true;
        currentBuilding = Instantiate(_movableBuildingsWithID[constructionID]);
    }
}
