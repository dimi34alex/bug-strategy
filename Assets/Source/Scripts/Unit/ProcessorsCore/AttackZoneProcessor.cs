using System;
using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.Unit.OrderValidatorCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.ProcessorsCore
{
    public class AttackZoneProcessor
    {
        private readonly UnitInteractionZone _attackZone;
        private readonly IAffiliation _affiliation;
        private readonly Dictionary<ITarget, IDamagable> _targets = new();

        public IReadOnlyDictionary<ITarget, IDamagable> Targets => _targets;
        public int EnemiesCount => _targets.Keys.Count;
        public float AttackRange { get; }
        
        public event Action OnEnterEnemyInZone;
        public event Action OnExitEnemyFromZone;

        public AttackZoneProcessor(UnitBase unit, float attackRange)
        {
            _affiliation = unit;
            AttackRange = attackRange;
            
            _attackZone = unit.DynamicUnitZone;
            _attackZone.SetRadius(AttackRange);
            _attackZone.EnterEvent += TryAddTarget;
            _attackZone.ExitEvent += TryRemoveTarget;
            
            foreach (var target in _attackZone.UnitTargets)
                TryAddTarget(target);
        }
        
        private void TryAddTarget(ITarget target)
        {
            if (target.IsAnyNull() ||
                target.Affiliation == _affiliation.Affiliation ||
                !target.TryCast(out IDamagable damageable) ||
                Targets.ContainsKey(target))
                return;

            target.OnDeactivation += TryRemoveTarget;
            _targets.Add(target, damageable);
            OnEnterEnemyInZone?.Invoke();
        }

        private void TryRemoveTarget(ITarget target)
        {
            target.OnDeactivation -= TryRemoveTarget;
            
            if (_targets.Remove(target))
                OnExitEnemyFromZone?.Invoke();
        }
    }
}