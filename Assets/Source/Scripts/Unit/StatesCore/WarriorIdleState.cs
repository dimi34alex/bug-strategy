using Unit.ProfessionsCore;

namespace Unit.States
{
    public class WarriorIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly UnitBase _unit;
        private readonly WarriorProfessionBase _warriorProfession;
        
        public WarriorIdleState(UnitBase unit, WarriorProfessionBase warriorProfession)
        {
            _unit = unit;
            _warriorProfession = warriorProfession;
        }

        public override void OnStateEnter()
        {
            _warriorProfession.AttackProcessor.OnEnterEnemyInZone += CheckEnemiesInZone;
            CheckEnemiesInZone();
        }

        public override void OnStateExit()
        {
            _warriorProfession.AttackProcessor.OnEnterEnemyInZone -= CheckEnemiesInZone;
        }

        public override void OnUpdate()
        {
            
        }

        private void CheckEnemiesInZone()
        {
            if (!_warriorProfession.AttackProcessor.CheckEnemiesInAttackZone()) return;
            
            _unit.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}