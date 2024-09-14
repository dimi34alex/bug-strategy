using System;
using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.Unit.OrderValidatorCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.ProcessorsCore 
{
    public class ProfessionInteractionZoneProcessor
    {
        private readonly UnitInteractionZone _interactionZone;
        
        private IReadOnlyList<ITarget> Targets => _interactionZone.UnitTargets;

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

        public bool Contains(ITarget target) 
            => !target.IsAnyNull() && Targets.Contains(t => t == target);
        
        private void OnEnterTargetInZone(ITarget target) => OnEnterInZone?.Invoke();
        
        private void OnExitTargetFromZone(ITarget target) => OnExitFromZone?.Invoke();
    }
}