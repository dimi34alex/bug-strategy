using BugStrategy.CustomInput;
using BugStrategy.Selection;
using BugStrategy.Unit.UnitSelection;
using BugStrategy.Unit.UnitSelection.TargetPositionMarker;
using CycleFramework.Execute;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit
{
    public class UnitsOrderer : CycleInitializerBase
    {
        [SerializeField] private LayerMask targetsLayers;
        
        [Inject] private readonly Selector _selector;
        [Inject] private readonly UnitsSelector _unitsSelector;
        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly UnitsTargetPositionMarkerFactory _unitsTargetPositionMarkerFactory;

        private static Camera Camera => Camera.main;
        
        protected override void OnUpdate() 
            => OrderProcess();

        private void OrderProcess()
        {
            var selectedUnits = _unitsSelector.GetSelectedUnits();
            if (_inputProvider.RmbDown && selectedUnits.Count > 0 && !_selector.IsSelectProcess)
            {
                var ray = Camera.ScreenPointToRay(_inputProvider.MousePosition);
                if (Physics.Raycast(ray, out var hit, 100F, targetsLayers, QueryTriggerInteraction.Ignore) 
                    && hit.collider.TryGetComponent(out ITarget target))
                {
                    foreach (var unit in selectedUnits)
                        unit.AutoGiveOrder(target);
                }
                else
                {
                    var targetPosition = _inputProvider.MousePosition;
                    targetPosition = Camera.ScreenToWorldPoint(targetPosition).XZ();

                    var unitPositions =
                        RingStepPositionGenerator.TakeRingsPositions(targetPosition, selectedUnits.Count);

                    for (int i = 0; i < selectedUnits.Count; i++)
                        selectedUnits[i].AutoGiveOrder(null, unitPositions[i]);

                    _unitsTargetPositionMarkerFactory.Create(targetPosition);
                }
            }
        }
    }
}