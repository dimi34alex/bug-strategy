using Projectiles;
using Projectiles.Factory;
using Unit.ProcessorsCore;

namespace Unit.Bees
{
    public class HoneyCatapultAttackProcessor : RangeAttackProcessor
    {
        private readonly float _damageRadius;
        private readonly ResourceStorage _stickTileNum;
        
        private float _constructionDamageScale;
        
        public HoneyCatapultAttackProcessor(UnitBase unit, float attackRange, float damage, float damageRadius, CooldownProcessor cooldownProcessor, ProjectileFactory projectilesFactory) 
            : base(unit, attackRange, damage, cooldownProcessor, ProjectileType.HoneyСatapultProjectile, projectilesFactory)
        {
            _damageRadius = damageRadius;
            _stickTileNum = new ResourceStorage(0,0);
        }

        public void SetConstructionDamageScale(float newScale)
            => _constructionDamageScale = newScale;

        public void SetStickTileNum(int newStickTileNum)
            => _stickTileNum.SetCapacity(newStickTileNum);
        
        protected override void InitProjectileData(ProjectileBase projectile, IUnitTarget target)
        {
            base.InitProjectileData(projectile, target);
            
            var honeyCatapultProjectile = projectile.Cast<HoneyCatapultProjectile>();

            honeyCatapultProjectile.SetDamageRadius(_damageRadius);
            honeyCatapultProjectile.SetConstructionDamageScale(_constructionDamageScale);
            TryMadeStickProjectile(honeyCatapultProjectile);
        }
        
        private void TryMadeStickProjectile(HoneyCatapultProjectile projectile)
        {
            _stickTileNum.ChangeValue(1);

            if (_stickTileNum.CurrentValue >= _stickTileNum.Capacity)
            {
                _stickTileNum.SetValue(0);
                
                projectile.SetSticky();
            }
        }
    }
}