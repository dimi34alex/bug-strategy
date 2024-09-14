using System;
using System.Collections.Generic;
using BugStrategy.Trigger;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class UnitVisibleZone : TriggerZone
    {
        [SerializeField] private List<ITarget> _targets = new List<ITarget>();
        private Func<ITarget, bool> _filter = t => true;

        protected override bool _refreshEnteredComponentsAfterExit { get; } = false;

        public new IReadOnlyList<ITarget> ContainsComponents => _targets;

        public new event Action<ITarget> EnterEvent;
        public new event Action<ITarget> ExitEvent;

        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable 
            => t => t is ITarget && _filter(t.Cast<ITarget>());

        public bool Contains(ITarget target) => _targets.Contains(target);
        public bool Contains(Predicate<ITarget> predicate) => _targets.Exists(predicate);

        public void SetFilter(Func<ITarget, bool> filter)
        {
            _filter = filter;
        }

        protected override void OnEnter(ITriggerable component)
        {
            ITarget target = component.Cast<ITarget>();

            _targets.Add(target);
            EnterEvent?.Invoke(target);
        }

        protected override void OnExit(ITriggerable component)
        {
            ITarget target = component.Cast<ITarget>();

            _targets.Remove(target);
            ExitEvent?.Invoke(target);
        }
    }
}