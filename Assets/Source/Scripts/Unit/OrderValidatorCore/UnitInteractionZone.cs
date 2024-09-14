using System;
using System.Collections.Generic;
using BugStrategy.Trigger;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.OrderValidatorCore
{
    [RequireComponent(typeof(SphereCollider))]
    public class UnitInteractionZone : TriggerZone
    {
        [SerializeField] private SphereCollider sphereCollider;
        
        protected override bool _refreshEnteredComponentsAfterExit => false;
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t.CastPossible<ITarget>(); 

        private readonly List<ITarget> _unitTargets = new List<ITarget>();
        

        public IReadOnlyList<ITarget> UnitTargets => _unitTargets;
        
        public new event Action<ITarget> EnterEvent;
        public new event Action<ITarget> ExitEvent;
        
        protected override void OnEnter(ITriggerable component)
        {
            var target = component.Cast<ITarget>();
            _unitTargets.Add(target);
            EnterEvent?.Invoke(target);
        }

        protected override void OnExit(ITriggerable component)
        {
            var target = component.Cast<ITarget>();
            _unitTargets.Remove(target);
            ExitEvent?.Invoke(target);
        }
        
        public void SetRadius(float newRadius) => sphereCollider.radius = newRadius;
    }
}