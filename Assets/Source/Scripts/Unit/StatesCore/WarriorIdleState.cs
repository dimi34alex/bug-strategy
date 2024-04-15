using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;

namespace Unit.States
{
    public class WarriorIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly MovingUnit _unit;
        private readonly AttackProcessorBase _attackProcessorBase;
        
        public WarriorIdleState(MovingUnit unit, AttackProcessorBase attackProcessorBase)
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
            
            _unit.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}