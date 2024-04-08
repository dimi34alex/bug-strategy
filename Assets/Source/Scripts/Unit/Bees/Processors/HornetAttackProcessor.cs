using System;
using Unit.ProcessorsCore;
using UnityEngine;

namespace Unit.Bees
{
    public class HornetAttackProcessor : AttackProcessorBase
    {
        private float _criticalDamageScale; 
        private bool _isCriticalAttack;
        
        public override event Action OnAttack;
        public override event Action<IUnitTarget> OnAttackTarget;

        public HornetAttackProcessor(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor) 
            : base(unit, attackRange, damage, cooldownProcessor)
        {
            
        }

        public void SetCriticalAttack()
            => _isCriticalAttack = true;

        public void SetCriticalDamageScale(float newScale)
            => _criticalDamageScale = newScale;
        
        protected override void Attack(IUnitTarget target)
        {
            if (target.TryCast(out IDamagable damageable))
            {
                var damageScale = 1f;
                if (_isCriticalAttack)
                {
                    _isCriticalAttack = false;
                    damageScale = _criticalDamageScale;
                }
 
                damageable.TakeDamage(this, damageScale);
                OnAttack?.Invoke();
                OnAttackTarget?.Invoke(target);
            }
#if UNITY_EDITOR
            else
                Debug.LogWarning($"Target {target} can't be attacked");
#endif
        }
    }
}