using BugStrategy.Effects;
using BugStrategy.Unit;
using CycleFramework.Extensions;

namespace BugStrategy.Projectiles
{
    public class BeeWaxTowerProjectile : ProjectileBase
    {
        public override ProjectileType ProjectileType => ProjectileType.BeeWaxTowerProjectile;
        
        protected override void CollideWithTarget(IUnitTarget target)
        {
            if (target.TryCast(out IEffectable effectable))
                effectable.EffectsProcessor.ApplyEffect(EffectType.MoveSpeedDown);
            
            if (target.TryCast(out IDamagable damageable) && damageable.IsAlive)
                damageable.TakeDamage(Attacker, this);

            ReturnInPool();
        }
    }
}