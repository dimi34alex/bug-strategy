using System;
using System.Linq;
using Constructions.LevelSystemCore;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.ConstructionInformation
{
    public class ConstructionInfoScreen : EntityInfoScreen
    {
        [SerializeField] private Button _upgradeButton;

        private ConstructionBase _construction;
        private UIConstructionsConfig _uiConstructionsConfig;
        private ConstructionProductsUIView _productsUIView;
        private ConstructionRecruitingUIView _recruitingUIView;
        private ConstructionActionsType _actionsType;
        private ConstructionCreationProductUIView _сonstructionCreationProductUIView;
        private ConstructionActionsUIView _actionsUIView;
        
        private void Awake()
        {
            OnAwake();
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            _uiConstructionsConfig = ConfigsRepository.FindConfig<UIConstructionsConfig>();

            _actionsUIView = UIScreenRepository.GetScreen<ConstructionActionsUIView>();
            _recruitingUIView = UIScreenRepository.GetScreen<ConstructionRecruitingUIView>();
            _productsUIView = UIScreenRepository.GetScreen<ConstructionProductsUIView>();
            
            _actionsUIView.ButtonClicked += SetActiveAction;
            _recruitingUIView.ButtonClicked += OnRecruiting;
            _productsUIView.ButtonClicked += OnProduct;
        
            _actionsUIView.BackButtonClicked += BackButtonsMenu;
            _recruitingUIView.BackButtonClicked += BackButtonsMenu;
            _productsUIView.BackButtonClicked += BackButtonsMenu;
        
            _сonstructionCreationProductUIView = UIScreenRepository.GetScreen<ConstructionCreationProductUIView>();
        }

        public void SetConstruction(ConstructionBase construction)
        {
            if (_construction == construction)
                return;

            _construction = construction;
            SetActiveAction(ConstructionActionsType.None);
        }
    
        private void UpdateView()
        {
            var constructionUIConfig = _uiConstructionsConfig.ConstructionsUIConfigs[_construction.ConstructionID];

            _сonstructionCreationProductUIView.CloseAll();

            SetHealthPointsInfo(constructionUIConfig.InfoSprite, _construction.HealthStorage);

            _actionsUIView.TurnOffButtons();
            _recruitingUIView.TurnOffButtons();
            _productsUIView.TurnOffButtons();

            Debug.Log(_actionsType);
            switch (_actionsType)
            {
                case ConstructionActionsType.None:
                    _actionsUIView.SetButtons(constructionUIConfig.ConstructionActionsDictionary, constructionUIConfig.ConstructionActions
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
                _сonstructionCreationProductUIView.ActivatePanel();
            }

            _сonstructionCreationProductUIView.ActivateBar();
        }
    
        private void BackButtonsMenu()
            => SetActiveAction(ConstructionActionsType.None);
    
        private void OnUpgradeButtonClicked()
        {
            if (_construction.TryCast(out IEvolveConstruction evolveConstruction))
                evolveConstruction.LevelSystem.TryLevelUp();
        }

        private void SetActiveAction(ConstructionActionsType actionsType)
        {
            _actionsType = actionsType;
            UpdateView();
        }

        private void OnRecruiting(UnitType unitType)
        {
            if (!_construction.TryCast(out IRecruitingConstruction recruitingConstruction))
                return;
        
            recruitingConstruction.RecruitUnit(unitType);
        }
    
        private void OnProduct(ConstructionProduct constructionProduct)
        {

        }
    }
}
