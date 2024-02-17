using Unit.Professions;

namespace Unit.Ants.States
{
    public class AntIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly AntBase _ant;
        
        private WarriorProfessionBase _warriorProfession;
        
        public AntIdleState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if ((_ant.CurProfessionType == ProfessionType.MeleeWarrior || _ant.CurProfessionType == ProfessionType.RangeWarrior) &&
                 _ant.Profession.TryCast(out _warriorProfession))
            {
                _warriorProfession.AttackProcessor.OnEnterEnemyInZone += CheckEnemiesInZone;
                CheckEnemiesInZone();
            }
        }

        public override void OnStateExit()
        {
            if (_warriorProfession != null)
            {
                _warriorProfession.AttackProcessor.OnEnterEnemyInZone -= CheckEnemiesInZone;
            }
        }

        public override void OnUpdate()
        {
            
        }

        private void CheckEnemiesInZone()
        {
            if (!_warriorProfession.AttackProcessor.CheckEnemiesInAttackZone()) return;
            
            _ant.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}