using BugStrategy.CustomInput;
using BugStrategy.Missions;
using BugStrategy.UI;
using BugStrategy.Unit.UnitSelection;
using CycleFramework.Execute;
using CycleFramework.Extensions;
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

        public bool IsSelectProcess { get; private set; }
        public Vector3 StartSelectPoint { get; private set; }
        public Vector3 CurrentSelectPoint { get; private set; }

        public Vector3 worldStartPos, worldEndPos;

        protected override void OnUpdate ()
        {
            if(_inputProvider.LmbDown && !_inputProvider.MouseCursorOverUi())
            {
                IsSelectProcess = true;
                StartSelectPoint = _inputProvider.MousePosition;
                worldStartPos = Camera.main.ScreenToWorldPoint(StartSelectPoint);
            }

            CurrentSelectPoint = _inputProvider.MousePosition;

            if(IsSelectProcess && _inputProvider.LmbUp)
            {
                IsSelectProcess = false;
                if(_inputProvider.MouseCursorOverUi())
                    return;

                worldEndPos = Camera.main.ScreenToWorldPoint(CurrentSelectPoint);

                var dist = Vector3.Distance(worldStartPos, worldEndPos);

                var prevSelectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                if(prevSelectedConstruction != null)
                    prevSelectedConstruction.Deselect();
                _missionData.ConstructionSelector.ResetSelection();

                if(dist >= distanceToBeHold)
                {
                    _unitsSelector.DeselectAll();
                    if(_unitsSelector.SelectUnits(worldStartPos, worldEndPos))
                        _uiController.SetScreen(_unitsSelector.GetSelectedUnits()[0]);
                    else
                        _uiController.SetScreen(UIScreenType.Gameplay);
                }
                else
                {
                    var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);

                    if(_missionData.ConstructionSelector.TrySelect(ray))
                    {
                        var selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
                        selectedConstruction.Select();
                        _uiController.SetScreen(selectedConstruction);
                    }
                    else
                    {
                        _unitsSelector.DeselectAll();
                        if(_unitsSelector.SelectUnits(worldStartPos, worldEndPos))
                            _uiController.SetScreen(_unitsSelector.GetSelectedUnits()[0]);
                        else
                            _uiController.SetScreen(UIScreenType.Gameplay);
                    }
                }
            }
        }

        public void UpdateWorldStartPos (Vector3 delta)
        {
            worldStartPos = new Vector3(worldStartPos.x + delta.x, worldStartPos.y, worldStartPos.z + delta.z);
            StartSelectPoint = Camera.main.WorldToScreenPoint(worldStartPos);
        }
    }
}