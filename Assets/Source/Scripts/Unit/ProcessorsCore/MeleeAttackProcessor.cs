using UnityEngine;

namespace Unit.ProcessorsCore
{
    public class MeleeAttackProcessor : AttackProcessorBase
    {
        public MeleeAttackProcessor(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor) 
            : base(unit, attackRange, damage, cooldownProcessor)
        { }
        
        protected override void Attack(IUnitTarget target)
        {
            if (target.TryCast(out IDamagable damageable))
                damageable.TakeDamage(this);
#if UNITY_EDITOR
            else
                Debug.LogWarning($"Target {target} can't be attacked");
#endif
        }
    }
}