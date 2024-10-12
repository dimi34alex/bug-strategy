﻿using System;
using System.Linq;
using BugStrategy.Constructions;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.ResourceProduceConstruction;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class ConstructionInfoScreen : EntityInfoScreen
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private GameObject _dopPanel;

        private ConstructionBase _construction;
        
        private ConstructionActionsType _actionsType;
        private ConstructionActionsUIView _actionsUIView;
        private ConstructionRecruitingUIView _recruitingUIView;
        private ConvertInfo _convertInfo;
        
        private ConstructionRecruitingProcessUIView _recruitingProcessUIView;

        private UIConstructionConfig _uiConstructionConfig;
        private UIConstructionsConfig _uiConstructionsConfig;
        
        private void Awake()
        {
            OnAwake();
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            _uiConstructionsConfig = ConfigsRepository.ConfigsRepository.FindConfig<UIConstructionsConfig>();

            _actionsUIView = GetComponentInChildren<ConstructionActionsUIView>();
            _recruitingUIView = GetComponentInChildren<ConstructionRecruitingUIView>();
            _convertInfo = GetComponentInChildren<ConvertInfo>(true);
            
            _actionsUIView.ButtonClicked += SetActionsType;
            _recruitingUIView.ButtonClicked += RecruitUnit;
        
            _actionsUIView.BackButtonClicked += SetNonActionsType;
            _recruitingUIView.BackButtonClicked += SetNonActionsType;
            _convertInfo.BackButtonClicked += SetNonActionsType;
        
            _recruitingProcessUIView = GetComponentInChildren<ConstructionRecruitingProcessUIView>();
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

            var recruitingConstruction = _construction.UnSafeCast<IRecruitingConstruction>();
            _recruitingProcessUIView.InitRecruiter(recruitingConstruction);
            
            SetActionsType(ConstructionActionsType.None);
        }
    
        private void UpdateView()
        {
            _recruitingProcessUIView.Hide();
            _dopPanel.SetActive(false);

            SetHealthPointsInfo(_uiConstructionConfig.InfoSprite, _construction.HealthStorage);

            _actionsUIView.TurnOffButtons();
            _recruitingUIView.TurnOffButtons();
            _convertInfo.Hide();

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
                    _convertInfo.Show(showBackButton, _construction as ResourceConversionConstructionBase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            if (_uiConstructionConfig.Recruiting != null && _uiConstructionConfig.Recruiting.Count > 0)
                _dopPanel.SetActive(true);

            _recruitingProcessUIView.ActivateBar();
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

        public override void Hide()
        {
            _convertInfo.Hide();
            base.Hide();
        }
    }
}
