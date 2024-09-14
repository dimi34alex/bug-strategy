using System;
using BugStrategy.EntityState;
using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit
{
    public class WarriorIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly UnitBase _unit;
        private readonly AttackProcessorBase _attackProcessorBase;
        
        public override event Action StateExecuted;
        
        public WarriorIdleState(UnitBase unit, AttackProcessorBase attackProcessorBase)
        {
            _unit = unit;
            _attackProcessorBase = attackProcessorBase;
        }

        public override void OnStateEnter()
        {
            _attackProcessorBase.OnEnterEnemyInZone += CheckEnemiesInZone;
            CheckEnemiesInZone();
        }

        public override void OnStateExit()
        {
            _attackProcessorBase.OnEnterEnemyInZone -= CheckEnemiesInZone;
        }

        public override void OnUpdate()
        {
            
        }

        private void CheckEnemiesInZone()
        {
            if (!_attackProcessorBase.CheckEnemiesInAttackZone()) return;
            
            StateExecuted?.Invoke();
            //_unit.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}