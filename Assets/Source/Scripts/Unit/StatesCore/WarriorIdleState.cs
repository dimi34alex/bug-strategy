using Unit.ProfessionsCore;

namespace Unit.States
{
    public class WarriorIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly MovingUnit _unit;
        private readonly WarriorOrderValidatorBase _warriorOrderValidator;
        
        public WarriorIdleState(MovingUnit unit, WarriorOrderValidatorBase warriorOrderValidator)
        {
            _unit = unit;
            _warriorOrderValidator = warriorOrderValidator;
        }

        public override void OnStateEnter()
        {
            _warriorOrderValidator.AttackProcessor.OnEnterEnemyInZone += CheckEnemiesInZone;
            CheckEnemiesInZone();
        }

        public override void OnStateExit()
        {
            _warriorOrderValidator.AttackProcessor.OnEnterEnemyInZone -= CheckEnemiesInZone;
        }

        public override void OnUpdate()
        {
            
        }

        private void CheckEnemiesInZone()
        {
            if (!_warriorOrderValidator.AttackProcessor.CheckEnemiesInAttackZone()) return;
            
            _unit.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}