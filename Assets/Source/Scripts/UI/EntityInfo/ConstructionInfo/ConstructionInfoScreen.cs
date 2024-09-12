using System;
using System.Linq;
using Constructions.LevelSystemCore;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.EntityInfo.ConstructionInfo
{
    public class ConstructionInfoScreen : EntityInfoScreen
    {
        [SerializeField] private Button _upgradeButton;

        private ConstructionBase _construction;
        
        private ConstructionActionsType _activeActionsType;
        private ConstructionActionsUIView _actionsUIView;
        private ConstructionProductsUIView _productsUIView;
        private ConstructionRecruitingUIView _recruitingUIView;
        
        private ConstructionCreationProductUIView _creationProductUIView;
        private UIConstructionsConfig _uiConstructionsConfig;
        
        private void Awake()
        {
            OnAwake();
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            _uiConstructionsConfig = ConfigsRepository.FindConfig<UIConstructionsConfig>();

            _actionsUIView = UIScreenRepository.GetScreen<ConstructionActionsUIView>();
            _recruitingUIView = UIScreenRepository.GetScreen<ConstructionRecruitingUIView>();
            _productsUIView = UIScreenRepository.GetScreen<ConstructionProductsUIView>();
            
            _actionsUIView.ButtonClicked += SetActionsType;
            _recruitingUIView.ButtonClicked += RecruitUnit;
            _productsUIView.ButtonClicked += ProductResource;
        
            _actionsUIView.BackButtonClicked += SetNonActionsType;
            _recruitingUIView.BackButtonClicked += SetNonActionsType;
            _productsUIView.BackButtonClicked += SetNonActionsType;
        
            _creationProductUIView = UIScreenRepository.GetScreen<ConstructionCreationProductUIView>();
        }

        public void SetConstruction(ConstructionBase construction)
        {
            if (_construction == construction)
                return;

            _construction = construction;
            SetActionsType(ConstructionActionsType.None);
        }
    
        private void UpdateView()
        {
            var constructionUIConfig = _uiConstructionsConfig.ConstructionsUIConfigs[_construction.ConstructionID];

            _creationProductUIView.CloseAll();

            SetHealthPointsInfo(constructionUIConfig.InfoSprite, _construction.HealthStorage);

            _actionsUIView.TurnOffButtons();
            _recruitingUIView.TurnOffButtons();
            _productsUIView.TurnOffButtons();
            
            Debug.Log(_activeActionsType);
            switch (_activeActionsType)
            {
                case ConstructionActionsType.None:
                    _actionsUIView.SetButtons(constructionUIConfig.ConstructionActionsDictionary, 
                        constructionUIConfig.ConstructionActions
                            .Select(x => x.Key).ToList());
                    
                    break;
                case ConstructionActionsType.RecruitUnits:
                    _recruitingUIView.SetButtons(constructionUIConfig.RecruitingDictionary, constructionUIConfig.Recruiting
                        .Select(x => x.Key).ToList());
                    break;
                case ConstructionActionsType.ProduceResources:
                    _productsUIView.SetButtons(constructionUIConfig.ConstructionProductsDictionary, constructionUIConfig.ConstructionProducts
                        .Select(x => x.Key).ToList());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            if ((constructionUIConfig.ConstructionProducts != null &&  constructionUIConfig.ConstructionProducts.Count > 0)||
                (constructionUIConfig.Recruiting != null && constructionUIConfig.Recruiting.Count > 0))
            {
                _creationProductUIView.ActivatePanel();
            }

            _creationProductUIView.ActivateBar();
        }
    
        private void SetNonActionsType()
            => SetActionsType(ConstructionActionsType.None);

        private void SetActionsType(ConstructionActionsType actionsType)
        {
            _activeActionsType = actionsType;
            UpdateView();
        }
        
        private void RecruitUnit(UnitType unitType)
        {
            if (!_construction.TryCast(out IRecruitingConstruction recruitingConstruction))
                return;
        
            recruitingConstruction.RecruitUnit(unitType);
        }
    
        private void ProductResource(ConstructionProductType constructionProductType)
        {

        }
        
        private void OnUpgradeButtonClicked()
        {
            if (_construction.TryCast(out IEvolveConstruction evolveConstruction))
                evolveConstruction.LevelSystem.TryLevelUp();
        }
    }
}
