using System;
using BugStrategy.Constructions;
using BugStrategy.CustomInput;
using BugStrategy.Missions;
using BugStrategy.UI;
using BugStrategy.Unit.UnitSelection;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Selection
{
    public class Selector : CycleInitializerBase
    {
        [SerializeField] private float distanceToBeHold;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly MissionData _missionData;
        [Inject] private readonly UIController _uiController;
        [Inject] private readonly PlayerUnitsSelector _playerUnitsSelector;
        [Inject] private readonly EnemyUnitsSelector _enemyUnitsSelector;
        
        public ConstructionSelector ConstructionSelector => _missionData.ConstructionSelector;
        public PlayerUnitsSelector PlayerUnitsSelector => _playerUnitsSelector;
        public EnemyUnitsSelector EnemyUnitsSelector => _enemyUnitsSelector;
        public bool IsSelectProcess { get; private set; }
        public Vector3 StartSelectPoint { get; private set; }
        public Vector3 CurrentSelectPoint { get; private set; }

        public Action OnTrySelect;

        protected override void OnUpdate()
        {
            if (_inputProvider.LmbDown && !_inputProvider.MouseCursorOverUi())
            {
                IsSelectProcess = true;
                StartSelectPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            }

            CurrentSelectPoint = Camera.main.ScreenToWorldPoint(_inputProvider.MousePosition);
            
            if (IsSelectProcess && _inputProvider.LmbUp)
            {
                IsSelectProcess = false;
                if (_inputProvider.MouseCursorOverUi())
                    return;

                var dist = Vector3.Distance(StartSelectPoint, CurrentSelectPoint);

                var prevSelectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                if (prevSelectedConstruction != null) 
                    prevSelectedConstruction.Deselect();
                
                if (dist >= distanceToBeHold)
                {
                    _missionData.ConstructionSelector.ResetSelection();
                    _playerUnitsSelector.DeselectAll();
                    _enemyUnitsSelector.DeselectAll();
                    
                    _enemyUnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation);
                    if (_playerUnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation))
                        _uiController.SetScreen(_playerUnitsSelector.GetSelectedUnits()[0]);
                    else
                        _uiController.SetScreen(UIScreenType.Gameplay);
                }
                else
                {
                    var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);
                    
                    if (_missionData.ConstructionSelector.TrySelect(ray, _missionData.PlayerAffiliation))
                    {
                        var selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                        selectedConstruction.Select();
                        _uiController.SetScreen(selectedConstruction);
                    }
                    else
                    {
                        _playerUnitsSelector.DeselectAll();
                        _enemyUnitsSelector.DeselectAll();

                        _enemyUnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation);
                        if (_playerUnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation))
                            _uiController.SetScreen(_playerUnitsSelector.GetSelectedUnits()[0]);
                        else
                            _uiController.SetScreen(UIScreenType.Gameplay);
                    }
                }
                
                OnTrySelect?.Invoke();
            }
        }
    }
}