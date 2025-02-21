using BugStrategy.Projectiles;
using BugStrategy.Projectiles.Factory;
using BugStrategy.TechnologiesSystem.Technologies;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.Bees
{
    public sealed class HoneyCatapultAttackProcessor : RangeAttackProcessor
    {
        private readonly float _damageRadius;
        private readonly FloatStorage _projectileCounter;
        
        private TechHoneyCatapult _techHoneyCatapult;
        private float _constructionDamageScale = 1;
        
        public HoneyCatapultAttackProcessor(UnitBase unit, float attackRange, float damage, float damageRadius, CooldownProcessor cooldownProcessor, ProjectilesFactory projectilesFactory) 
            : base(unit, attackRange, damage, cooldownProcessor, ProjectileType.HoneyCatapultProjectile, projectilesFactory)
        {
            _damageRadius = damageRadius;
            _projectileCounter = new FloatStorage(0, 0);
        }

        public void SetTech(TechHoneyCatapult techHoneyCatapult) 
            => _techHoneyCatapult = techHoneyCatapult;

        public void SetConstructionDamageScale(float newScale)
            => _constructionDamageScale = newScale;

        public void SetProjectileCounterCapacity(int newProjectileCounterCapacity)
            => _projectileCounter.SetCapacity(newProjectileCounterCapacity);
        
        protected override void InitProjectileData(ProjectileBase projectile, ITarget target)
        {
            base.InitProjectileData(projectile, target);
            
            var honeyCatapultProjectile = projectile.Cast<HoneyCatapultProjectile>();

            honeyCatapultProjectile.SetDamageRadius(_damageRadius);
            honeyCatapultProjectile.SetConstructionDamageScale(_constructionDamageScale);
            TryMadeProjectileSticky(honeyCatapultProjectile);
        }
        
        private void TryMadeProjectileSticky(HoneyCatapultProjectile projectile)
        {
            if (_techHoneyCatapult.Researched) 
                _projectileCounter.ChangeValue(1);

            if (_projectileCounter.CurrentValue >= _projectileCounter.Capacity)
            {
                _projectileCounter.SetValue(0);
                
                projectile.SetSticky();
            }
        }
    }
}