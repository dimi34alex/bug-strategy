using BugStrategy.Projectiles;
using BugStrategy.Projectiles.Factory;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.Bees
{
    public sealed class HorntailAttackProcessor : RangeAttackProcessor
    {
        private readonly float _damageRadius;
        
        public HorntailAttackProcessor(UnitBase unit, float attackRange, float damage, float damageRadius, CooldownProcessor cooldownProcessor, ProjectileFactory projectilesFactory) 
            : base(unit, attackRange, damage, cooldownProcessor, ProjectileType.HorntailProjectile, projectilesFactory)
        {
            _damageRadius = damageRadius;
        }

        protected override void InitProjectileData(ProjectileBase projectile, ITarget target)
        {
            base.InitProjectileData(projectile, target);
            
            projectile.Cast<HorntailProjectile>().SetDamageRadius(_damageRadius);
        }
    }
}