using Projectiles;
using Projectiles.Factory;
using Unit.ProcessorsCore;

namespace Unit.Bees
{
    public class SawyerAttackProcessor : RangeAttackProcessor
    {
        private float _exitDamageScale = 1;
        
        public SawyerAttackProcessor(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor,
            ProjectileType projectileType, ProjectileFactory projectilesFactory) 
            : base(unit, attackRange, damage, cooldownProcessor, projectileType, projectilesFactory)
        {
        }

        public void SetExitDamageScale(float exitDamageScale)
        {
            _exitDamageScale = exitDamageScale;
        }
        
        protected override void InitProjectileData(ProjectileBase projectile, IUnitTarget target)
        {
            projectile.SetDamage(Damage * _exitDamageScale);
            projectile.SetTarget(target);
        }
    }
}