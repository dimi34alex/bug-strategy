using System;
using System.Collections.Generic;
using Unit.OrderValidatorCore;

namespace Unit.ProcessorsCore
{
    public class AttackZoneProcessor
    {
        private readonly UnitInteractionZone _attackZone;
        private readonly AffiliationEnum _affiliation;
        private readonly Dictionary<IUnitTarget, IDamagable> _targets = new Dictionary<IUnitTarget, IDamagable>();

        public IReadOnlyDictionary<IUnitTarget, IDamagable> Targets => _targets;
        public int EnemiesCount => _targets.Keys.Count;
        public float AttackRange { get; }
        
        public event Action OnEnterEnemyInZone;
        public event Action OnExitEnemyFromZone;

        public AttackZoneProcessor(UnitBase unit, float attackRange)
        {
            _affiliation = unit.Affiliation;
            AttackRange = attackRange;
            
            _attackZone = unit.DynamicUnitZone;
            _attackZone.SetRadius(AttackRange);
            _attackZone.EnterEvent += OnEnterTargetInZone;
            _attackZone.ExitEvent += OnExitTargetFromZone;
            
            foreach (var target in _attackZone.UnitTargets)
                OnEnterTargetInZone(target);
        }
        
        protected void OnEnterTargetInZone(IUnitTarget target)
        {
            if (target.IsAnyNull() ||
                target.Affiliation == _affiliation ||
                !target.TryCast(out IDamagable damageable) ||
                Targets.ContainsKey(target))
                return;

            _targets.Add(target, damageable);
            OnEnterEnemyInZone?.Invoke();
        }

        protected void OnExitTargetFromZone(IUnitTarget target)
        {
            if (_targets.Remove(target))
                OnExitEnemyFromZone?.Invoke();
        }
    }
}