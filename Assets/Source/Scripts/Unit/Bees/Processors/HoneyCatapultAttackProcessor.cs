using Projectiles;
using Projectiles.Factory;
using Unit.ProcessorsCore;

namespace Unit.Bees
{
    public sealed class HoneyCatapultAttackProcessor : RangeAttackProcessor
    {
        private readonly float _damageRadius;
        private readonly ResourceStorage _projectileCounter;
        
        private float _constructionDamageScale = 1;
        
        public HoneyCatapultAttackProcessor(UnitBase unit, float attackRange, float damage, float damageRadius, CooldownProcessor cooldownProcessor, ProjectileFactory projectilesFactory) 
            : base(unit, attackRange, damage, cooldownProcessor, ProjectileType.HoneyCatapultProjectile, projectilesFactory)
        {
            _damageRadius = damageRadius;
            _projectileCounter = new ResourceStorage(0, 0);
        }

        public void SetConstructionDamageScale(float newScale)
            => _constructionDamageScale = newScale;

        public void SetProjectileCounterCapacity(int newProjectileCounterCapacity)
            => _projectileCounter.SetCapacity(newProjectileCounterCapacity);
        
        protected override void InitProjectileData(ProjectileBase projectile, IUnitTarget target)
        {
            base.InitProjectileData(projectile, target);
            
            var honeyCatapultProjectile = projectile.Cast<HoneyCatapultProjectile>();

            honeyCatapultProjectile.SetDamageRadius(_damageRadius);
            honeyCatapultProjectile.SetConstructionDamageScale(_constructionDamageScale);
            TryMadeProjectileSticky(honeyCatapultProjectile);
        }
        
        private void TryMadeProjectileSticky(HoneyCatapultProjectile projectile)
        {
            _projectileCounter.ChangeValue(1);

            if (_projectileCounter.CurrentValue >= _projectileCounter.Capacity)
            {
                _projectileCounter.SetValue(0);
                
                projectile.SetSticky();
            }
        }
    }
}