using System;
using System.Linq;
using Source.Scripts.Unit;
using UnityEngine;

namespace Source.Scripts.UI.EntityInfo.UnitInfo
{
    public class UnitInfoScreen : EntityInfoScreen
    {
        private UIUnitsConfig _uiUnitsConfig;
        private UnitActionsUIView _actionsUIView;
        private TacticsUIView _tacticsUIView;
        private BuldingsUIView _buldingsUIView;
        private UnitBase _unit;
        private UnitActionsType _actionsType;

        private UserBuilder _builder;
        
        private void Awake()
        {
            _builder = GameObject.Find("Builder").GetComponent<UserBuilder>();
            if(_builder == null)
                Debug.LogError("Builder is null");
            
            OnAwake();
            _uiUnitsConfig = ConfigsRepository.FindConfig<UIUnitsConfig>();

            _actionsUIView = UIScreenRepository.GetScreen<UnitActionsUIView>();
            _tacticsUIView = UIScreenRepository.GetScreen<TacticsUIView>();
            _buldingsUIView = UIScreenRepository.GetScreen<BuldingsUIView>();
            
            _actionsUIView.ButtonClicked += SetActiveAction;
            _buldingsUIView.ButtonClicked += OnBuldingInstance;
            _tacticsUIView.ButtonClicked += OnTacticsUse;
        
            _actionsUIView.BackButtonClicked += BackButtonsMenu;
            _buldingsUIView.BackButtonClicked += BackButtonsMenu;
            _tacticsUIView.BackButtonClicked += BackButtonsMenu;
        }

        public void SetUnit(UnitBase unit)
        {
            if (_unit == unit)
                return;

            _unit = unit;
            SetActiveAction(UnitActionsType.None);
        }
    
        private void UpdateView()
        {
            UnitType unitType = _unit.UnitType;

            try
            {
                UIUnitConfig unitUIConfig = _uiUnitsConfig.UnitsUIConfigs[unitType];

                SetHealthPointsInfo(unitUIConfig.InfoSprite, _unit.HealthStorage);
                _actionsUIView.TurnOffButtons();
                _tacticsUIView.TurnOffButtons();
                _buldingsUIView.TurnOffButtons();

                var onlyOneActionsType = unitUIConfig.Actions.Count == 1;
                var showBackButton = !onlyOneActionsType;
                if (onlyOneActionsType)
                    _actionsType = unitUIConfig.Actions.First().Key;
                
                switch (_actionsType)
                {
                    case UnitActionsType.None:
                        _actionsUIView.SetButtons(showBackButton, unitUIConfig.UnitSectionsDictionary,
                            unitUIConfig.Actions
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Tactics:
                        _tacticsUIView.SetButtons(showBackButton, unitUIConfig.UnitTacticsDictionary,
                            unitUIConfig.UnitTactics
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Constructions:
                        _buldingsUIView.SetButtons(showBackButton, unitUIConfig.UnitConstructionDictionary,
                            unitUIConfig.UnitConstruction
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Abilities:

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("Настоятельно рекомендую проверить есть ли конфиг (UIUnitConfig и добавлен ли он " +
                                    "в UIRaceConfig)  " + exp.Message);
            }
        }
    
        private void BackButtonsMenu()
            => SetActiveAction(UnitActionsType.None);
    
        private void SetActiveAction(UnitActionsType actionsType)
        {
            _actionsType = actionsType;
            UpdateView();
        }
        
        private void OnTacticsUse(UnitTacticType unitTacticType)
        {
            switch (unitTacticType)
            {
                case UnitTacticType.Build:
               
                    break;
                case UnitTacticType.Repair:
                    // _unitBase.AutoGiveOrder();
                    break;
            }
        }
        
        private void OnBuldingInstance(ConstructionID constructionID) 
            => _builder.SpawnConstructionMovableModel(constructionID);
    }
}