using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using System.Linq;
using Constructions;

public class UserBuilder : CycleInitializerBase
{
    [Inject] private readonly ConstructionsConfigsRepository _constructionsConfigsRepository;
    [Inject] private readonly IConstructionFactory _constructionFactory;
    [Inject] private readonly DiContainer _diContainer;
    
    [SerializeField] private SerializableDictionary<ConstructionID, GameObject> constructionMovableModels;
    [SerializeField] private ConstructionBase[] constructions;
    private Dictionary<ConstructionID, ConstructionBase> _constructionWithID;

    private GameObject _currentConstructionMovableModel;
    private ConstructionID _currentConstructionID;

    private UIController _UIController;

    private bool _spawnConstruction = false;
    private float _numberTownHall = 0;
    
    protected override void OnInit()
    {
        _UIController = UIScreenRepository.GetScreen<UIController>();
        _constructionWithID = constructions.ToDictionary(x => x.ConstructionID, x => x);
        
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
    }

    protected override void OnUpdate()
    {
        if (_spawnConstruction)
        {
            MoveConstructionMovableModel();
        }
        else
        {
            Main();
        }
    }

    private void Main()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(FrameworkCommander.GlobalData.ConstructionSelector.TrySelect(ray))
            {
                ConstructionBase selectedConstruction = FrameworkCommander.GlobalData.ConstructionSelector.SelectedConstruction;
                selectedConstruction.Select();
                _UIController.SetBuilding(selectedConstruction);
            }
            else if (!MouseCursorOverUI())
            {
                _UIController.SetWindow(UIWindowType.GameMain);
            }
        }
    }

    private bool MouseCursorOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void MoveConstructionMovableModel()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!MouseCursorOverUI() && Physics.Raycast(ray, out hit, 100F, CustomLayerID.Construction_Ground.Cast<int>(), QueryTriggerInteraction.Ignore)) //если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
        {
            _currentConstructionMovableModel.transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

            if (Input.GetButtonDown("Fire1"))//подтверждение строительства здания
            {
                if (hit.collider.name == "TileBase")
                {
                    if (!hit.collider.GetComponent<Tile>().Visible)
                    {
                        Destroy(_currentConstructionMovableModel);
                        _spawnConstruction = false;
                        return;
                    }
                }
                
                foreach (UnitBase unit in FrameworkCommander.GlobalData.UnitRepository.AllUnits)
                {
                    if (unit.IsSelected && unit.gameObject.CompareTag("Worker") && CanBuyConstruction(_currentConstructionID))
                    {
                        BuyConstruction(_currentConstructionID);

                        if (TrySpawnConstruction(_currentConstructionID, out var buildingProgressConstruction))
                            unit.HandleGiveOrder(buildingProgressConstruction, UnitPathType.Build_Construction);

                        Destroy(_currentConstructionMovableModel);
                        _spawnConstruction = false;
                        break;
                    }
                }
            }
            else if (Input.GetButtonDown("Fire2"))//отмена начала строительства
            {
                Destroy(_currentConstructionMovableModel);
                _spawnConstruction = false;
            }
        }
    }
    
    private bool CanBuyConstruction(ConstructionID id )
    {
        bool flagCanBuy = true;

        foreach (var element in _constructionsConfigsRepository.TakeBuyCost(id).ResourceCost)
             if (element.Value > ResourceGlobalStorage.GetResource(element.Key).CurrentValue)
                 flagCanBuy = false;

        return flagCanBuy;
    }

    private void BuyConstruction(ConstructionID id)
    {
        foreach (var element in _constructionsConfigsRepository.TakeBuyCost(id).ResourceCost)
            ResourceGlobalStorage.GetResource(element.Key).SetValue(ResourceGlobalStorage.GetResource(element.Key).CurrentValue - element.Value);
    }
    
    private bool TrySpawnConstruction(ConstructionID id, out BuildingProgressConstruction construction)
    {
        construction = null;

        if (id == ConstructionID.Bees_Town_Hall && _numberTownHall >= 1)
            return false;
        
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);
        if (index <= -1) 
            return false;
        
        Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);
        if (FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(position, false))
            return false;

        if (id == ConstructionID.Bees_Town_Hall)
            _numberTownHall++;
        
        construction = SpawnConstruction(id, position);
        
        return true;
    }
    
    private BuildingProgressConstruction SpawnConstruction(ConstructionID id, Vector3 position)
    {
        BuildingProgressConstruction progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.Building_Progress_Construction);
        progressConstruction.transform.position = position;
        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, progressConstruction);
                
        progressConstruction.OnTimerEnd += c => CreateConstruction(c, position);

        progressConstruction.StartBuilding(4, id);

        return progressConstruction;
    }

    private void CreateConstruction(BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
    {
        ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(buildingProgressConstruction.BuildingConstructionID);
        
        FrameworkCommander.GlobalData.ConstructionsRepository.GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, construction);
        construction.transform.position = position;
    }

    public void SpawnConstructionMovableModel(ConstructionID constructionID)
    {
        if (_currentConstructionMovableModel != null)
        {
            Destroy(_currentConstructionMovableModel.gameObject);
        }

        _currentConstructionID = constructionID;
        _spawnConstruction = true;
        _currentConstructionMovableModel = Instantiate(constructionMovableModels[constructionID]);
    }
}