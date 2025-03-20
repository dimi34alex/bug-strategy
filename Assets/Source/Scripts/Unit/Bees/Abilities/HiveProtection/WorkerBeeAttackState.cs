using System;
using BugStrategy.EntityState;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit
{
    public class WorkerBeeAttackState : EntityStateBase, IDamageApplicator
    {
        public override EntityStateID EntityStateID => EntityStateID.Attack;
        public float Damage => 999;

        private readonly UnitBase _unit;

        private readonly AttackProcessorBase _attackProcessor;
        private readonly IReadOnlyCooldownProcessor _cooldownProcessor;

        private bool CanAttack => !_cooldownProcessor.IsCooldown; 
        
        public override event Action StateExecuted;
        
        public WorkerBeeAttackState(UnitBase unit, WarriorOrderValidator warriorOrderValidator)
        {
            _unit = unit;
            _attackProcessor = warriorOrderValidator.AttackProcessor;
            _cooldownProcessor = warriorOrderValidator.Cooldown;
        }
        
        public WorkerBeeAttackState(UnitBase unit, AttackProcessorBase attackProcessor, IReadOnlyCooldownProcessor cooldownProcessor)
        {
            _unit = unit;
            _attackProcessor = attackProcessor;
            _cooldownProcessor = cooldownProcessor;
        }
        
        public override void OnStateEnter()
        {
            if(!_attackProcessor.CheckEnemiesInAttackZone())
            {
                StateExecuted?.Invoke();
                return;
            }
            
            _cooldownProcessor.OnCooldownEnd += TryAttack;
            _attackProcessor.OnExitEnemyFromZone += OnExitEnemyFromZone;

            if(CanAttack) 
                TryAttack();
        }

        public override void OnStateExit()
        {
            _cooldownProcessor.OnCooldownEnd -= TryAttack;
            _attackProcessor.OnExitEnemyFromZone -= OnExitEnemyFromZone;
        }

        public override void OnUpdate()
        {
            
        }
        
        private void TryAttack()
        {
            _attackProcessor.TryAttack(_unit.CurrentPathData.Target);
            _unit.TakeDamage(this, 1);
        }

        private void OnExitEnemyFromZone()
        {
            StateExecuted?.Invoke();
        }
    }
}