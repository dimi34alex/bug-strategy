using System;
using BugStrategy.Projectiles;
using BugStrategy.Projectiles.Factory;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.ProcessorsCore
{
    public class RangeAttackProcessor : AttackProcessorBase
    {
        private readonly ProjectileType _projectileType;
        private readonly ProjectilesFactory _projectilesFactory;
        
        public override event Action Attacked;
        public override event Action<ITarget> TargetAttacked;
        public event Action<ProjectileBase> ProjectileSpawned; 
        
        public RangeAttackProcessor(UnitBase unit, float attackRange, float damage, CooldownProcessor cooldownProcessor, 
            ProjectileType projectileType, ProjectilesFactory projectilesFactory)
            : base(unit, attackRange, damage, cooldownProcessor)
        {
            _projectileType = projectileType;
            _projectilesFactory = projectilesFactory;
        }
        
        protected sealed override void Attack(ITarget target)
        {
            if (!target.CastPossible<IDamagable>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Target {target} can't be attacked");
#endif
                return;
            }

            var projectile = _projectilesFactory.Create(_projectileType);
            projectile.transform.position = Transform.position;
            InitProjectileData(projectile, target);
            ProjectileSpawned?.Invoke(projectile);
            Attacked?.Invoke();
            TargetAttacked?.Invoke(target);
        }

        protected virtual void InitProjectileData(ProjectileBase projectile, ITarget target)
        {
            projectile.Init(Affiliation, Attacker, this);
            projectile.SetTarget(target);
        }
    }
}