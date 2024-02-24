using System;
using System.Collections.Generic;
using Unit.ProfessionsCore;

namespace Unit.ProfessionsCore.Processors
{
    public class ProfessionInteractionZoneProcessor
    {
        private readonly ProfessionInteractionZone _interactionZone;
        
        private IReadOnlyList<IUnitTarget> Targets => _interactionZone.UnitTargets;

        public float InteractionRange { get; }
        
        public event Action OnEnterInZone;
        public event Action OnExitFromZone;

        public ProfessionInteractionZoneProcessor(ProfessionInteractionZone professionInteractionZone, float interactionRange)
        {
            InteractionRange = interactionRange;
            
            _interactionZone = professionInteractionZone;
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