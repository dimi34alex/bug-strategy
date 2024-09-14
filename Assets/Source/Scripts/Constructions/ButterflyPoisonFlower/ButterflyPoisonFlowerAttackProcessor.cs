using System.Collections.Generic;
using BugStrategy.Projectiles;
using BugStrategy.Projectiles.Factory;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using DG.Tweening;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyPoisonFlower
{
    public class ButterflyPoisonFlowerAttackProcessor
    {
        private readonly IUnitTarget _shooter;
        private readonly IAffiliation _affiliation;
        private readonly List<UnitBase> _enemies = new List<UnitBase>();
        private readonly ProjectileFactory _projectileFactory;
        private readonly TriggerBehaviour _triggerBehaviour;
        private readonly Transform _flowerPosition;
        
        private Sequence _cooldownTimer;
        private float _attackCooldown;
        private float _attackDamage;
        private float _damageRadius;

        public AffiliationEnum Affiliation => _affiliation.Affiliation;
        
        public ButterflyPoisonFlowerAttackProcessor(IAffiliation affiliation, Transform flowerPosition, 
            ProjectileFactory projectileFactory, TriggerBehaviour triggerBehaviour, IUnitTarget shooter)
        {
            _affiliation = affiliation;
            _flowerPosition = flowerPosition;
            _projectileFactory = projectileFactory;
            _triggerBehaviour = triggerBehaviour;
            _shooter = shooter;

            _triggerBehaviour.EnterEvent += OnUnitEnter;
            _triggerBehaviour.ExitEvent += OnUnitExit;
            
            _cooldownTimer = DOTween.Sequence()
                .SetUpdate(UpdateType.Manual)
                .AppendInterval(0);
        }

        public void HandleUpdate(float time)
        {
            AttackCooldownTick(time);
        }

        public void SetData(float newAttackCooldown, float newAttackDamage, float newDamageRadius)
        {
            _attackCooldown = newAttackCooldown;
            _attackDamage = newAttackDamage;
            _damageRadius = newDamageRadius;
        }
        
        public void KillCooldownTimer()
            => _cooldownTimer.Kill();

        private void AttackCooldownTick(float time)
            => _cooldownTimer?.ManualUpdate(time, time);
        
        private void TryAttack()
        {
            if (_enemies.Count <= 0)
                return;

            UnitBase target = null;
            float distance = float.MaxValue;
            foreach (var enemy in _enemies)
            {
                var newDistance = Vector3.Distance(_flowerPosition.position, enemy.transform.position);
                if (newDistance <= distance)
                {
                    target = enemy;
                    distance = newDistance;
                }
            }
            
            var projectile = _projectileFactory.Create(ProjectileType.ButterflyPoisonFlowerProjectile).Cast<ButterflyPoisonFlowerProjectile>();
            projectile.SetTarget(target);
            projectile.Init(Affiliation, _shooter, _attackDamage);
            projectile.SetDamageRadius(_damageRadius);
            projectile.transform.position = _flowerPosition.position;
            
            if (_enemies.Count > 0)
            {
                _cooldownTimer = DOTween.Sequence()
                    .SetUpdate(UpdateType.Manual)
                    .AppendInterval(_attackCooldown)
                    .AppendCallback(TryAttack);
            }
        }
        
        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                if (Affiliation.CheckEnemies(unit.Affiliation) 
                    || !_enemies.Contains(unit))
                {
                    _enemies.Add(unit);
                    
                    if(!_cooldownTimer.active)
                        TryAttack();
                }
            }
        }

        private void OnUnitExit(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                _enemies.Remove(unit);

                if (_enemies.Count <= 0)
                    _cooldownTimer?.Kill();
            }
        }
    }
}