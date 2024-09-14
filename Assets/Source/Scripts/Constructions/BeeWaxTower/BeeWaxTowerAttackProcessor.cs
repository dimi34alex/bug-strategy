using System;
using System.Collections.Generic;
using BugStrategy.CustomTimer;
using BugStrategy.Libs;
using BugStrategy.Projectiles;
using BugStrategy.Projectiles.Factory;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Constructions.BeeWaxTower
{
    public class BeeWaxTowerAttackProcessor
    {
        private readonly IAffiliation _affiliation;
        private readonly SpawnProcessor _spawnProcessor;
        private readonly TriggerBehaviour _attackZone;
        private readonly List<ITarget> _targets;
        private readonly Transform _spawnTransform;
        private readonly Timer _cooldown;
        
        private int _projectilesCount;

        public AffiliationEnum Affiliation => _affiliation.Affiliation;
        
        public BeeWaxTowerAttackProcessor(IAffiliation affiliation, ProjectileFactory projectileFactory, 
            TriggerBehaviour attackZone, Transform spawnTransform, ITarget shooter)
        {
            _affiliation = affiliation;
            _attackZone = attackZone;
            _spawnTransform = spawnTransform;
            _targets = new List<ITarget>();

            _spawnProcessor = new SpawnProcessor(_affiliation, projectileFactory, spawnTransform, shooter);
            _spawnProcessor.OnEndSpawn += ResetCooldown;
            
            _attackZone.EnterEvent += OnTargetEnter;
            _attackZone.ExitEvent += OnTargetExit;

            _cooldown = new Timer(0, 0, true);
            _cooldown.OnTimerEnd += TryAttack;
        }

        public void HandleUpdate(float time)
        {
            _spawnProcessor.HandleUpdate(time);
            _cooldown.Tick(time);
        }
        
        public void SetData(int projectilesCount, float cooldown, float spawnPause, float damage, ProjectileType projectileType)
        {
            _projectilesCount = projectilesCount;
            _cooldown.SetMaxValue(cooldown, false);
            
            _spawnProcessor.SetData(spawnPause, damage, projectileType);
        }

        private void OnTargetEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out ITarget target) 
                && Affiliation.CheckEnemies(target.Affiliation) 
                && target.CastPossible<IDamagable>())
            {
                if(_targets.Contains(target))
                    return;

                target.OnDeactivation += TryRemoveTarget;
                _targets.Add(target);
                
                if(_cooldown.TimerIsEnd)
                    TryAttack();
            }
        }
        
        private void OnTargetExit(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out ITarget target))
                TryRemoveTarget(target);
        }

        private void TryRemoveTarget(ITarget target)
        {
            target.OnDeactivation -= TryRemoveTarget;
            
            if (Affiliation.CheckEnemies(target.Affiliation) 
                && target.CastPossible<IDamagable>())
            {
                _targets.Remove(target);
            }
        }
        
        private void TryAttack()
        {
            if(_targets.Count <= 0)
                return;

            List<(ITarget, float)> targetsWithDistance = new List<(ITarget, float)>();

            foreach (var target in _targets)
                targetsWithDistance.Add((target, Vector3.Distance(target.Transform.position, _spawnTransform.position)));

            targetsWithDistance.Sort(
                (x, y) =>
                {
                    if (x.Item2 < y.Item2)
                        return 0;
                    else
                        return 1;
                }
            );

            List<ITarget> tars = new List<ITarget>(); 
            int targetIndex = 0;
            for (int i = 0; i < _projectilesCount; i++)
            {
                tars.Add(targetsWithDistance[targetIndex].Item1);
                targetIndex++;
                if (targetIndex >= _targets.Count)
                    targetIndex = 0;
            }
            
            _spawnProcessor.InvokeAttack(tars);
        }

        private void ResetCooldown()
            => _cooldown.Reset();
        
        private class SpawnProcessor
        {
            private readonly ITarget _shooter;
            private readonly IAffiliation _affiliation;
            private readonly ProjectileFactory _projectileFactory;
            private readonly TriggerBehaviour _attackZone;
            private readonly Transform _spawnTransform;
            private readonly Timer _spawnPauseTimer;
            
            private ProjectileType _projectileType;
            private List<ITarget> _targets;
            private float _damage;

            public AffiliationEnum Affiliation => _affiliation.Affiliation;
            
            public event Action OnEndSpawn;
            
            public SpawnProcessor(IAffiliation affiliation, ProjectileFactory projectileFactory, Transform spawnTransform, 
                ITarget shooter)
            {
                _affiliation = affiliation;
                _projectileFactory = projectileFactory;
                _spawnTransform = spawnTransform;
                _shooter = shooter;
                _spawnPauseTimer = new Timer(0, 0, true);
                _spawnPauseTimer.OnTimerEnd += SpawnProjectile;
            }

            public void HandleUpdate(float time)
                => _spawnPauseTimer.Tick(time);
            
            public void SetData(float spawnPause, float damage, ProjectileType projectileType)
            {
                _spawnPauseTimer.SetMaxValue(spawnPause, false);
                _projectileType = projectileType;
                _damage = damage;
            }
            
            public void InvokeAttack(List<ITarget> targets)
            {
                _targets = targets;
                SpawnProjectile();
            }
            
            private void SpawnProjectile()
            {
                if (_targets == null || _targets.Count <= 0)
                    return;
                
                var target = _targets[0];
                _targets.RemoveAt(0);
                
                if(target.IsAnyNull() || !target.IsActive)
                    return;
                
                var projectile = _projectileFactory.Create(_projectileType).Cast<BeeWaxTowerProjectile>();
                projectile.SetTarget(target);
                projectile.Init(Affiliation, _shooter, _damage);
                projectile.transform.position = _spawnTransform.position;
                
                if(_targets.Count > 0)
                    _spawnPauseTimer.Reset();
                else
                    OnEndSpawn?.Invoke();
            }
        }
    }
}