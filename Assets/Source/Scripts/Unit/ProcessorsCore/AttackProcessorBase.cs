using System;
using BugStrategy.Constructions;
using BugStrategy.Constructions.BeeStickyTile;
using BugStrategy.Libs;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.ProcessorsCore
{
    public abstract class AttackProcessorBase : IReadOnlyAttackProcessor, IDamageApplicator
    {
        private readonly CooldownProcessor _cooldownProcessor;
        private readonly AttackZoneProcessor _attackZoneProcessor;
        private readonly IAffiliation _affiliation;
        protected readonly Transform Transform;
        protected readonly ITarget Attacker;
        
        public int EnemiesCount => _attackZoneProcessor.EnemiesCount;
        public float AttackRange => _attackZoneProcessor.AttackRange;
        protected AffiliationEnum Affiliation => _affiliation.Affiliation;
        public float Damage { get; }

        public abstract event Action Attacked;
        public abstract event Action<ITarget> TargetAttacked;
        public event Action OnEnterEnemyInZone;
        public event Action OnExitEnemyFromZone;

        protected AttackProcessorBase(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor)
        {
            Transform = unit.Transform;
            _affiliation = unit;
            Damage = damage;
            
            _cooldownProcessor = cooldownProcessor;
            Attacker = unit;

            _attackZoneProcessor = new AttackZoneProcessor(unit, attackRange);
            _attackZoneProcessor.OnEnterEnemyInZone += EnterEnemyInZone;
            _attackZoneProcessor.OnExitEnemyFromZone += ExitEnemyFromZone;
        }

        public bool TargetInZone(ITarget someTarget)
        {
            if(someTarget.IsAnyNull() || !someTarget.IsActive)
                return false;
            
            return _attackZoneProcessor.Targets.ContainsKey(someTarget);
        }

        /// <returns>
        /// return true if distance between unit and someTarget less or equal attack range, else return false
        /// </returns>
        public bool CheckAttackDistance(ITarget someTarget) => _attackZoneProcessor.Targets.ContainsKey(someTarget);

        /// <returns> return tru if some IDamageable stay in attack zone, else return false</returns>
        public bool CheckEnemiesInAttackZone() => EnemiesCount > 0;

        /// <summary>
        /// Attack target, if target can't be attacked, then attack nearest enemy
        /// </summary>
        public void TryAttack(ITarget target)
        {
            if (_cooldownProcessor.IsCooldown) return;
            
            if (!target.IsAnyNull() && CheckAttackDistance(target) && target.CastPossible<IDamagable>() 
                || TryGetNearestDamageableTarget(out target))
            {
                if(!target.Transform.gameObject.TryGetComponent(out BeeStickyTile bst))
                    Attack(target);
                _cooldownProcessor.StartCooldown();
            }
        }
        
        protected abstract void Attack(ITarget target);

        private bool TryGetNearestDamageableTarget(out ITarget nearestTarget)
        {
            nearestTarget = null;
            float currentDistance = float.MaxValue;

            foreach (var target in _attackZoneProcessor.Targets)
            {
                float distance = Distance(target.Key);
                if (distance < currentDistance)
                {
                    nearestTarget = target.Key;
                    currentDistance = distance;
                }
            }

            return !(nearestTarget is null);
        }
        
        private float Distance(ITarget someTarget) => Vector3.Distance(Transform.position, someTarget.Transform.position);

        private void EnterEnemyInZone() => OnEnterEnemyInZone?.Invoke();

        private void ExitEnemyFromZone() => OnExitEnemyFromZone?.Invoke();
    }
}