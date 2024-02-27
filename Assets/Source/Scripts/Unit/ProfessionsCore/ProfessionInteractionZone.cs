using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.ProfessionsCore
{
    [RequireComponent(typeof(SphereCollider))]
    public class ProfessionInteractionZone : TriggerZone
    {
        [SerializeField] private SphereCollider sphereCollider;
        
        protected override bool _refreshEnteredComponentsAfterExit => false;
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t.CastPossible<IUnitTarget>(); 

        private readonly List<IUnitTarget> _unitTargets = new List<IUnitTarget>();
        

        public IReadOnlyList<IUnitTarget> UnitTargets => _unitTargets;
        
        public new event Action<IUnitTarget> EnterEvent;
        public new event Action<IUnitTarget> ExitEvent;
        
        protected override void OnEnter(ITriggerable component)
        {
            var target = component.Cast<IUnitTarget>();
            _unitTargets.Add(target);
            EnterEvent?.Invoke(target);
        }

        protected override void OnExit(ITriggerable component)
        {
            var target = component.Cast<IUnitTarget>();
            _unitTargets.Remove(target);
            ExitEvent?.Invoke(target);
        }
        
        public void SetRadius(float newRadius) => sphereCollider.radius = newRadius;
    }
}