using Projectiles;
using UnityEngine;

namespace Unit.Professions.Processors
{
    public class RangeAttackProcessor : AttackProcessorBase
    {
        private readonly ProjectileType _projectileType;
        private readonly ProjectilesPool _projectilesPool;
        
        public RangeAttackProcessor(UnitBase unit, float attackRange, float damage,
            CooldownProcessor cooldownProcessor, ProjectileType projectileType, ProjectilesPool projectilesPool)
            : base(unit, attackRange, damage, cooldownProcessor)
        {
            _projectileType = projectileType;
            _projectilesPool = projectilesPool;
        }
        
        protected override void Attack(IUnitTarget target)
        {
            if (!target.CastPossible<IDamagable>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Target {target} can't be attacked");
#endif
                return;
            }

            var projectile = _projectilesPool.Extract<ProjectileBase>(_projectileType);
            projectile.transform.position = Transform.position;
            projectile.gameObject.SetActive(true);
            projectile.SetTarget(target);
        }
    }
}