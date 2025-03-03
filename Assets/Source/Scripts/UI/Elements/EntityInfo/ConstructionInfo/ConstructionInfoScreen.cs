using System;
using System.Linq;
using BugStrategy.Constructions;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.ResourceProduceConstruction;
using BugStrategy.UI.Elements.EntityInfo.ConstructionInfo.AntWorkshopViews;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;
using CycleFramework.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class ConstructionInfoScreen : EntityInfoScreen
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _demolitionButton;
        [SerializeField] private GameObject _dopPanel;

        private ConstructionBase _construction;
        
        private ConstructionActionsType _actionsType;
        private ConstructionActionsUIView _actionsUIView;
        private ConstructionRecruitingUIView _recruitingUIView;
        
        private ConstructionResourceConversionUIView _resourceConversionUIView;
        private ConstructionRecruitingProcessUIView _recruitingProcessUIView;

        private UIConstructionConfig _uiConstructionConfig;
        private UIConstructionsConfig _uiConstructionsConfig;
        
        private AntWorkshopView _antWorkshopView;
        
        private void Awake()
        {
            OnAwake();
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            _demolitionButton.onClick.AddListener(OnDemolitionButtonClicked);
            _uiConstructionsConfig = ConfigsRepository.ConfigsRepository.FindConfig<UIConstructionsConfig>();

            _actionsUIView = GetComponentInChildren<ConstructionActionsUIView>();
            _recruitingUIView = GetComponentInChildren<ConstructionRecruitingUIView>();
            _resourceConversionUIView = GetComponentInChildren<ConstructionResourceConversionUIView>(true);
            
            _actionsUIView.ButtonClicked += SetActionsType;
            _recruitingUIView.ButtonClicked += RecruitUnit;
        
            _actionsUIView.BackButtonClicked += SetNonActionsType;
            _recruitingUIView.BackButtonClicked += SetNonActionsType;
            _resourceConversionUIView.BackButtonClicked += SetNonActionsType;
        
            _recruitingProcessUIView = GetComponentInChildren<ConstructionRecruitingProcessUIView>();
            
            _antWorkshopView = GetComponentInChildren<AntWorkshopView>();
            _antWorkshopView.BackButtonClicked += SetNonActionsType;
        }

        public void SetConstruction(ConstructionBase newConstruction)
        {
            try
            {
                _construction = newConstruction;
                _uiConstructionConfig = _uiConstructionsConfig.ConstructionsUIConfigs[_construction.ConstructionID];
            }
            catch (Exception exp)
            {
                throw new Exception(
                    $"Настоятельно рекомендую проверить есть ли конфиг ({nameof(UIConstructionConfig)} " +
                    $"и добавлен ли он в {nameof(UIConstructionConfig)}) | {exp.Message}");
            }

            ToggleDemolitionButtonVisibility(_construction);
            SetActionsType(ConstructionActionsType.None);
        }
    
        private void UpdateView()
        {
            _recruitingProcessUIView.Hide();
            _dopPanel.SetActive(false);

            SetHealthPointsInfo(_uiConstructionConfig.InfoSprite, _construction.HealthStorage);

            _actionsUIView.TurnOffButtons();
            _recruitingUIView.TurnOffButtons();
            _resourceConversionUIView.Hide();
            _antWorkshopView.Hide();

            var onlyOneActionsType = _uiConstructionConfig.ConstructionActions.Count == 1;
            var showBackButton = !onlyOneActionsType;
            if (onlyOneActionsType)
                _actionsType = _uiConstructionConfig.ConstructionActions.First().Key;
            
            switch (_actionsType)
            {
                case ConstructionActionsType.None:
                    _actionsUIView.SetButtons(showBackButton, _uiConstructionConfig.ConstructionActionsDictionary, 
                        _uiConstructionConfig.ConstructionActions
                            .Select(x => x.Key).ToList());
                    break;
                case ConstructionActionsType.RecruitUnits:
                    _recruitingUIView.SetButtons(showBackButton, _uiConstructionConfig.RecruitingDictionary,
                        _uiConstructionConfig.Recruiting
                            .Select(x => x.Key).ToList());
                    break;
                case ConstructionActionsType.ProduceResources:
                    _resourceConversionUIView.Show(showBackButton, _construction as ResourceConversionConstructionBase);
                    break;
                case ConstructionActionsType.AntsProfessions:
                    _antWorkshopView.Show(showBackButton, _construction as AntWorkshopBase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            if (_uiConstructionConfig.Recruiting != null && _uiConstructionConfig.Recruiting.Count > 0)
                _dopPanel.SetActive(true);

            var recruitingConstruction = _construction.UnSafeCast<IRecruitingConstruction>();
            _recruitingProcessUIView.InitRecruiter(recruitingConstruction);
        }
    
        private void SetNonActionsType()
            => SetActionsType(ConstructionActionsType.None);

        private void SetActionsType(ConstructionActionsType newActionsType)
        {
            _actionsType = newActionsType;
            UpdateView();
        }
        
        private void RecruitUnit(UnitType unitType)
        {
            if (!_construction.TryCast(out IRecruitingConstruction recruitingConstruction))
                return;
        
            recruitingConstruction.RecruitUnit(unitType);
        }
        
        private void OnUpgradeButtonClicked()
        {
            if (_construction.TryCast(out IEvolveConstruction evolveConstruction))
                evolveConstruction.LevelSystem.TryLevelUp();
        }

        private void OnDemolitionButtonClicked()
        {
            if (_construction.CastPossible<TownHallBase>())
                return;

            _construction.Demolition();
            Hide();
        }

        private void ToggleDemolitionButtonVisibility(ConstructionBase construction) 
            => _demolitionButton.gameObject.SetActive(!_construction.CastPossible<TownHallBase>());

        public override void Hide()
        {
            _resourceConversionUIView.Hide();
            base.Hide();
        }
    }
}
