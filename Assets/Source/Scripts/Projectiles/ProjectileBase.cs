using System;
using BugStrategy.Libs;
using BugStrategy.Pool;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour, IDamageApplicator, IPoolable<ProjectileBase, ProjectileType>,
        IPoolEventListener
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }

        public float Damage { get; private set; }
        public abstract ProjectileType ProjectileType { get; }
        public ProjectileType Identifier => ProjectileType;
        public AffiliationEnum Affiliation { get; private set; }
        
        protected ITarget Target;
        protected ITarget Attacker;

        public event Action<ProjectileBase> ElementReturnEvent;
        public event Action<ProjectileBase> ElementDestroyEvent;

        public void HandleUpdate(float time)
        {
            CheckTargetOnNull();
            Move(time);
        }

        public void Init(AffiliationEnum affiliation, ITarget attacker, IDamageApplicator damageApplicator)
            => Init(affiliation, attacker, damageApplicator.Damage);
        
        public void Init(AffiliationEnum affiliation, ITarget attacker, float damage)
        {
            Affiliation = affiliation;
            Attacker = attacker;
            Damage = damage;

            Attacker.OnDeactivation += (_) => Attacker = null;
        }

        public void SetTarget(ITarget target)
        {
            Target = target;
            Target.OnDeactivation += OnTargetDeactivation;
        }

        public virtual void OnElementReturn() 
            => gameObject.SetActive(false);

        public virtual void OnElementExtract() 
            => gameObject.SetActive(true);

        private void OnTargetDeactivation(ITarget _)
        {
            Target.OnDeactivation -= OnTargetDeactivation;
            ReturnInPool();
        }
        
        protected void CheckTargetOnNull()
        {
            if(Target.IsAnyNull())
                ReturnInPool();
        }
        
        protected virtual void CollideWithTarget(ITarget target)
        {
            if (target.TryCast(out IDamagable damagable))
                damagable.TakeDamage(Attacker, this);

            ReturnInPool();
        }

        protected void ReturnInPool() => ElementReturnEvent?.Invoke(this);
        
        protected virtual void Move(float time)
        {
            var step = MoveSpeed * time;
            transform.position = Vector3.MoveTowards(transform.position, Target.Transform.position, step);
        }

        private void OnTriggerEnter(Collider someCollider)
        {
            if (someCollider.TryGetComponent(out ITarget target) && target == Target)
                CollideWithTarget(target);
        }
        
        private void OnDestroy()
        {
            ElementDestroyEvent?.Invoke(this);
        }
    }
}