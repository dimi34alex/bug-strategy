using System;
using System.Linq;
using Source.Scripts.Unit;
using Source.Scripts.Unit.AbilitiesCore;
using UnityEngine;

namespace Source.Scripts.UI.EntityInfo.UnitInfo
{
    public class UnitInfoScreen : EntityInfoScreen
    {
        private UnitBase _unit;
        
        private UnitActionsType _actionsType;
        private UnitActionsUIView _actionsUIView;
        private ConstructUIView _constructUIView;
        private TacticsUIView _tacticsUIView;
        private AbilitiesUIView _abilitiesUIView;

        private UIUnitConfig _uiUnitConfig;
        private UIUnitsConfig _uiUnitsConfig;
        private AbilitiesUiConfig _abilitiesUiConfig;
        
        private UserBuilder _builder;
        
        private void Awake()
        {
            //TODO: remove this shit with "Find"
            _builder = GameObject.Find("Builder").GetComponent<UserBuilder>();
            if(_builder == null)
                Debug.LogError("Builder is null");
            
            OnAwake();
            
            _uiUnitsConfig = ConfigsRepository.FindConfig<UIUnitsConfig>();
            _abilitiesUiConfig = ConfigsRepository.FindConfig<AbilitiesUiConfig>();
            
            _actionsUIView = UIScreenRepository.GetScreen<UnitActionsUIView>();
            _tacticsUIView = UIScreenRepository.GetScreen<TacticsUIView>();
            _constructUIView = UIScreenRepository.GetScreen<ConstructUIView>();
            _abilitiesUIView = UIScreenRepository.GetScreen<AbilitiesUIView>();
            
            _actionsUIView.ButtonClicked += SetActionsType;
            _constructUIView.ButtonClicked += CreateConstruction;
            _tacticsUIView.ButtonClicked += ActivateTactic;
            _abilitiesUIView.ButtonClicked += ActivateAbility;
        
            _actionsUIView.BackButtonClicked += SetNonActionsType;
            _constructUIView.BackButtonClicked += SetNonActionsType;
            _tacticsUIView.BackButtonClicked += SetNonActionsType;
            _abilitiesUIView.BackButtonClicked += SetNonActionsType;
        }

        public void SetUnit(UnitBase newUnit)
        {
            if (_unit == newUnit)
                return;
            
            try
            {
                _unit = newUnit;
                _uiUnitConfig = _uiUnitsConfig.UnitsUIConfigs[_unit.UnitType];
            }
            catch (Exception exp)
            {
                throw new Exception(
                    $"Настоятельно рекомендую проверить есть ли конфиг ({nameof(UIUnitConfig)} " +
                    $"и добавлен ли он в {nameof(UIUnitsConfig)}) | {exp.Message}");
            }

            SetActionsType(UnitActionsType.None);
        }

        private void UpdateView()
        {
            try
            {
                SetHealthPointsInfo(_uiUnitConfig.InfoSprite, _unit.HealthStorage);

                _actionsUIView.TurnOffButtons();
                _tacticsUIView.TurnOffButtons();
                _constructUIView.TurnOffButtons();
                _abilitiesUIView.TurnOffButtons();

                var onlyOneActionsType = _uiUnitConfig.Actions.Count == 1;
                var showBackButton = !onlyOneActionsType;
                if (onlyOneActionsType)
                    _actionsType = _uiUnitConfig.Actions.First().Key;
                
                switch (_actionsType)
                {
                    case UnitActionsType.None:
                        _actionsUIView.SetButtons(showBackButton, _uiUnitConfig.UnitSectionsDictionary,
                            _uiUnitConfig.Actions
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Tactics:
                        _tacticsUIView.SetButtons(showBackButton, _uiUnitConfig.UnitTacticsDictionary,
                            _uiUnitConfig.UnitTactics
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Constructions:
                        _constructUIView.SetButtons(showBackButton, _uiUnitConfig.UnitConstructionDictionary,
                            _uiUnitConfig.UnitConstruction
                                .Select(x => x.Key).ToList());
                        break;
                    case UnitActionsType.Abilities:
                        ShowAbilities(showBackButton);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception exp)
            {
                throw new Exception($"{exp.Message}");
            }
        }

        private void SetNonActionsType()
            => SetActionsType(UnitActionsType.None);
    
        private void SetActionsType(UnitActionsType newActionsType)
        {
            _actionsType = newActionsType;
            UpdateView();
        }
        
        private void ActivateTactic(UnitTacticType tacticType)
        {
            switch (tacticType)
            {
                case UnitTacticType.Build:
               
                    break;
                case UnitTacticType.Repair:
                    // _unitBase.AutoGiveOrder();
                    break;
            }
        }

        private void ShowAbilities(bool showBackButton)
        {
            if (!_unit.TryCast(out IAbilitiesOwner abilitiesOwner))
                return;

            if (abilitiesOwner.ActiveAbilities.Count <= 0 
                && abilitiesOwner.PassiveAbilities.Count <= 0 
                && _uiUnitConfig.Actions.Count != 1)//this line required to avoid infinite recursion
            {
                SetActionsType(UnitActionsType.None);
                return;
            }

            var abilities = abilitiesOwner.ActiveAbilities
                .Select(ability => ability.AbilityType).ToList();
            var passiveAbilities = abilitiesOwner.PassiveAbilities
                .Select(ability => ability.AbilityType).ToList();
            
            abilities.AddRange(passiveAbilities);
            _abilitiesUIView.SetButtons(showBackButton, _abilitiesUiConfig.AbilitiesUiIcons, abilities);
        }
        
        private void ActivateAbility(AbilityType ability)
        {
            if (!_unit.TryCast(out IAbilitiesOwner abilitiesOwner))
                return;
            
            abilitiesOwner.ActivateAbility(ability);
        }
        
        private void CreateConstruction(ConstructionID constructionID) 
            => _builder.SpawnConstructionMovableModel(constructionID);
    }
}