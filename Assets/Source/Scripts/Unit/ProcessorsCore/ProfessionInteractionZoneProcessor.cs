using System;
using System.Collections.Generic;
using Unit.OrderValidatorCore;

namespace Unit.ProcessorsCore 
{
    public class ProfessionInteractionZoneProcessor
    {
        private readonly UnitInteractionZone _interactionZone;
        
        private IReadOnlyList<IUnitTarget> Targets => _interactionZone.UnitTargets;

        public float InteractionRange { get; }
        
        public event Action OnEnterInZone;
        public event Action OnExitFromZone;

        public ProfessionInteractionZoneProcessor(UnitInteractionZone unitInteractionZone, float interactionRange)
        {
            InteractionRange = interactionRange;
            
            _interactionZone = unitInteractionZone;
            _interactionZone.SetRadius(interactionRange);
            _interactionZone.EnterEvent += OnEnterTargetInZone;
            _interactionZone.ExitEvent += OnExitTargetFromZone;
            
            foreach (var target in _interactionZone.UnitTargets)
                OnEnterTargetInZone(target);
        }

        public bool Contains(IUnitTarget unitTarget) 
            => !unitTarget.IsAnyNull() && Targets.Contains(t => t == unitTarget);
        
        private void OnEnterTargetInZone(IUnitTarget target) => OnEnterInZone?.Invoke();
        
        private void OnExitTargetFromZone(IUnitTarget target) => OnExitFromZone?.Invoke();
    }
}