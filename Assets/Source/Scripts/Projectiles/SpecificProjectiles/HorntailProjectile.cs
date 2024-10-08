using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Projectiles
{
    public sealed class HorntailProjectile : ProjectileBase
    {
        [SerializeField] private LayerMask layerMask;

        public override ProjectileType ProjectileType => ProjectileType.HorntailProjectile;

        private float _damageRadius;

        public void SetDamageRadius(float radius)
            => _damageRadius = radius;
        
        protected override void CollideWithTarget(ITarget target)
        {
            RaycastHit[] result = new RaycastHit[30];
            var size = Physics.SphereCastNonAlloc(transform.position, _damageRadius, Vector3.down,
                result, 0, layerMask);
            
            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) 
                    && damageable.IsAlive
                    && Affiliation.CheckEnemies(damageable.Affiliation))
                {
                    damageable.TakeDamage(Attacker, this);
                }
            }
            
            ReturnInPool();
        }
    }
}