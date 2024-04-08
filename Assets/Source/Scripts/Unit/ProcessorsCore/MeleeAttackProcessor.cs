using System;
using UnityEngine;

namespace Unit.ProcessorsCore
{
    public class MeleeAttackProcessor : AttackProcessorBase
    {
        public override event Action OnAttack;
        public override event Action<IUnitTarget> OnAttackTarget;

        public MeleeAttackProcessor(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor) 
            : base(unit, attackRange, damage, cooldownProcessor)
        { }
        
        protected override void Attack(IUnitTarget target)
        {
            if (target.TryCast(out IDamagable damageable))
            {
                damageable.TakeDamage(this);
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