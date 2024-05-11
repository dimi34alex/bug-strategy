using Unit.Effects;

namespace Projectiles
{
    public class BeeWaxTowerProjectile : ProjectileBase
    {
        public override ProjectileType ProjectileType => ProjectileType.BeeWaxTowerProjectile;
        
        protected override void CollideWithTarget(IUnitTarget target)
        {
            if (target.TryCast(out IEffectable effectable))
                effectable.EffectsProcessor.ApplyEffect(EffectType.MoveSpeedDown);
            
            if (target.TryCast(out IDamagable damageable))
                damageable.TakeDamage(Attacker, this);

            ReturnInPool();
        }
    }
}