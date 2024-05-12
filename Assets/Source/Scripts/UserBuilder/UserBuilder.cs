using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using Constructions;

public class UserBuilder : CycleInitializerBase
{
    [Inject] private readonly ConstructionsConfigsRepository _constructionsConfigsRepository;
    [Inject] private readonly IConstructionFactory _constructionFactory;
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;
    
    [SerializeField] private SerializableDictionary<ConstructionID, GameObject> constructionMovableModels;

    private GameObject _currentConstructionMovableModel;
    private ConstructionID _currentConstructionID;

    private bool _spawnConstruction;
    private float _numberTownHall;
    private UnitPool _pool;
    
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
                    if (unit.IsSelected && unit.gameObject.CompareTag("Worker") && CanBuyConstruction(unit.Affiliation, _currentConstructionID))
                    {
                        BuyConstruction(unit.Affiliation, _currentConstructionID);

                        if (TrySpawnConstruction(unit.Affiliation, _currentConstructionID, out var buildingProgressConstruction))
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
    
    private bool CanBuyConstruction(AffiliationEnum affiliation,ConstructionID id )
    {
        bool flagCanBuy = true;

        foreach (var element in _constructionsConfigsRepository.TakeBuyCost(id).ResourceCost)
             if (element.Value > _resourceGlobalStorage.GetResource(affiliation, element.Key).CurrentValue)
                 flagCanBuy = false;

        return flagCanBuy;
    }

    private void BuyConstruction(AffiliationEnum affiliation, ConstructionID id)
    {
        foreach (var element in _constructionsConfigsRepository.TakeBuyCost(id).ResourceCost)
            _resourceGlobalStorage.GetResource(affiliation, element.Key).SetValue(_resourceGlobalStorage.GetResource(affiliation, element.Key).CurrentValue - element.Value);
    }
    
    private bool TrySpawnConstruction(AffiliationEnum affiliation, ConstructionID id, out BuildingProgressConstruction construction)
    {
        construction = null;

        if (id == ConstructionID.BeeTownHall && _numberTownHall >= 1)
            return false;
        
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);
        if (index <= -1) 
            return false;
        
        Vector3 position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);
        if (FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(position, false))
            return false;

        if (id == ConstructionID.BeeTownHall)
            _numberTownHall++;
        
        construction = SpawnConstruction(affiliation, id, position);
        
        return true;
    }
    
    private BuildingProgressConstruction SpawnConstruction(AffiliationEnum affiliation, ConstructionID id, Vector3 position)
    {
        BuildingProgressConstruction progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.BuildingProgressConstruction, affiliation);
        progressConstruction.transform.position = position;
        FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(position, progressConstruction);
                
        progressConstruction.OnTimerEnd += c => CreateConstruction(affiliation, c, position);

        progressConstruction.StartBuilding(4, id);

        return progressConstruction;
    }

    private void CreateConstruction(AffiliationEnum affiliation, BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
    {
        ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(buildingProgressConstruction.BuildingConstructionID, affiliation);
        
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