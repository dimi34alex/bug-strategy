using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Projectiles
{
    public class ButterflyPoisonFlowerProjectile : ProjectileBase
    {
        [SerializeField] private LayerMask layerMask;
        
        public override ProjectileType ProjectileType => ProjectileType.ButterflyPoisonFlowerProjectile;

        private float _damageRadius;
        
        public void SetDamageRadius(float damageRadius)
        {
            _damageRadius = damageRadius;
        }
        
        protected override void CollideWithTarget(ITarget target)
        {
            if (target.TryCast(out IDamagable mainDamageable) && mainDamageable.IsAlive)
                mainDamageable.TakeDamage(Attacker, this);

            if (_damageRadius > 0)
            {
                RaycastHit[] result = new RaycastHit[30];
                var size = Physics.SphereCastNonAlloc(transform.position, _damageRadius, Vector3.down,
                    result, 0, layerMask);

                for (int i = 0; i < size; i++)
                {
                    if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) 
                        && Affiliation.CheckEnemies(damageable.Affiliation) 
                        && damageable != mainDamageable)
                    {
                        damageable.TakeDamage(Attacker, this, 0.5f);
                    }
                }  
            }
            
            ReturnInPool();
        }

        public override void OnElementReturn()
        {
            base.OnElementReturn();
            _damageRadius = 0;
        }
    }
}