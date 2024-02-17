using System;
using UnityEngine;

namespace Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour, IDamageApplicator, IPoolable<ProjectileBase, ProjectileType>
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }

        public abstract ProjectileType Identifier { get; }
        
        protected IUnitTarget Target;

        public event Action<ProjectileBase> ElementReturnEvent;
        public event Action<ProjectileBase> ElementDestroyEvent;

        public void HandleUpdate(float time)
        {
            CheckTagetOnNull();
            Move(time);
        }

        public void SetTarget(IUnitTarget target)
        {
            Target = target;
        }

        protected void CheckTagetOnNull()
        {
            if(Target.IsAnyNull())
                ReturnInPool();
        }
        
        protected virtual void CollideWithTarget(IUnitTarget target)
        {
            if (target.TryCast(out IDamagable damagable))
                damagable.TakeDamage(this);

            gameObject.SetActive(false);
            ReturnInPool();
        }

        protected void ReturnInPool() => ElementReturnEvent?.Invoke(this);
        
        protected virtual void Move(float time)
        {
            var step = MoveSpeed * time;
            transform.position = Vector3.MoveTowards(transform.position, Target.Transform.position, step);
        }
        
        void OnTriggerEnter(Collider someCollider)
        {
            if (someCollider.TryGetComponent(out IUnitTarget target) && target == Target)
                CollideWithTarget(target);
        }
        
        private void OnDestroy()
        {
            ElementDestroyEvent?.Invoke(this);
        }
    }
}