using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.CustomInput;
using BugStrategy.Libs;
using BugStrategy.Missions;
using BugStrategy.Tiles;
using BugStrategy.UI;
using BugStrategy.Unit;
using BugStrategy.Unit.UnitSelection;
using CycleFramework.Execute;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy
{
    public class UserBuilder : CycleInitializerBase
    {
        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly UIController _uiController;
        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly ConstructionsConfigsRepository _constructionsConfigsRepository;
        [Inject] private readonly IConstructionFactory _constructionFactory;
     
        [SerializeField] private SerializableDictionary<ConstructionID, GameObject> constructionMovableModels;

        private GameObject _currentConstructionMovableModel;
        private ConstructionID _currentConstructionID;
        private bool _spawnConstruction;
        private float _numberTownHall;
    
        protected override void OnUpdate()
        {
            if (_spawnConstruction)
                MoveConstructionMovableModel();
            else
                ObjectSelection();
        }

        private void ObjectSelection()
        {
            if (_inputProvider.LmbDown && !_inputProvider.MouseCursorOverUi())
            {
                var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);

                var prevSelectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                if (prevSelectedConstruction != null) 
                    prevSelectedConstruction.Deselect();
            
                if(_missionData.ConstructionSelector.TrySelect(ray))
                {
                    var selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                    selectedConstruction.Select();
                    UnitSelection.Instance.DeselectAllWithoutCheck();
                    _uiController.SetScreen(selectedConstruction);
                }
                else if (!_inputProvider.MouseCursorOverUi())
                {
                    _uiController.SetScreen(UIScreenType.Gameplay);
                }
            }
        }

        private void MoveConstructionMovableModel()
        {
            var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);
            if (!_inputProvider.MouseCursorOverUi() && Physics.Raycast(ray, out var hit, 100F, CustomLayerID.Construction_Ground.Cast<int>(), QueryTriggerInteraction.Ignore))
            {
                _currentConstructionMovableModel.transform.position = _missionData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

                if (_inputProvider.LmbDown)//подтверждение строительства здания
                {
                    if (hit.collider.name == "TileBase")
                    {
                        if (!hit.collider.GetComponent<Tile>().IsVisible)
                        {
                            Destroy(_currentConstructionMovableModel);
                            _spawnConstruction = false;
                            return;
                        }
                    }
                
                    foreach (var unit in _missionData.UnitRepository.AllUnits)
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
                else if (_inputProvider.RmbDown)//отмена начала строительства
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
                if (element.Value > _missionData.TeamsResourcesGlobalStorage.GetResource(affiliation, element.Key).CurrentValue)
                    flagCanBuy = false;

            return flagCanBuy;
        }

        private void BuyConstruction(AffiliationEnum affiliation, ConstructionID id)
        {
            foreach (var element in _constructionsConfigsRepository.TakeBuyCost(id).ResourceCost)
                _missionData.TeamsResourcesGlobalStorage.ChangeValue(affiliation, element.Key, element.Value);
        }
    
        private bool TrySpawnConstruction(AffiliationEnum affiliation, ConstructionID id, out BuildingProgressConstruction construction)
        {
            construction = null;

            if (id == ConstructionID.BeeTownHall && _numberTownHall >= 1)
                return false;
        
            RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(_inputProvider.MousePosition));
            int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);
            if (index <= -1) 
                return false;
        
            Vector3 position = _missionData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);
            if (_missionData.ConstructionsRepository.ConstructionExist(position, false))
                return false;

            if (id == ConstructionID.BeeTownHall)
                _numberTownHall++;
        
            construction = SpawnConstruction(affiliation, id, position);
        
            return true;
        }
    
        private BuildingProgressConstruction SpawnConstruction(AffiliationEnum affiliation, ConstructionID id, Vector3 position)
        {
            var progressConstruction = _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.BuildingProgressConstruction, position, affiliation);
                
            progressConstruction.OnTimerEnd += c => CreateConstruction(affiliation, c, position);

            progressConstruction.StartBuilding(4, id);

            return progressConstruction;
        }

        private void CreateConstruction(AffiliationEnum affiliation, BuildingProgressConstruction buildingProgressConstruction, Vector3 position)
        {
            _missionData.ConstructionsRepository.GetConstruction(position, true);
            Destroy(buildingProgressConstruction.gameObject);

            _constructionFactory.Create<ConstructionBase>(buildingProgressConstruction.BuildingConstructionID, position, affiliation);
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
}