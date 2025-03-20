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
        [Inject] private readonly UnitsSelector _unitsSelector;
        [Inject] private readonly ConstructionSelector _constructionSelector;

        public ConstructionSelector ConstructionSelector => _constructionSelector;
        public UnitsSelector UnitsSelector => _unitsSelector;
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

                _constructionSelector.Deselect();
                if (dist >= distanceToBeHold)
                {
                    UnitsSelector.DeselectAll();
                    
                    if (UnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation))
                        _uiController.SetScreen(UnitsSelector.GetPlayerSelectedUnits()[0]);
                    else
                        _uiController.SetScreen(UIScreenType.Gameplay);
                }
                else
                {
                    var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);
                    
                    if (_constructionSelector.TrySelect(ray, _missionData.PlayerAffiliation))
                    {
                        _uiController.SetScreen(_constructionSelector.SelectedConstruction);
                    }
                    else
                    {
                        UnitsSelector.DeselectAll();

                        if (UnitsSelector.SelectUnits(StartSelectPoint, CurrentSelectPoint, _missionData.PlayerAffiliation))
                            _uiController.SetScreen(UnitsSelector.GetPlayerSelectedUnits()[0]);
                        else
                            _uiController.SetScreen(UIScreenType.Gameplay);
                    }
                }
                
                OnTrySelect?.Invoke();
            }
        }
    }
}