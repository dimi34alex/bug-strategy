using Unit.Ants.Professions;
using Unit.OrderValidatorCore;

namespace Unit.Ants.States
{
    public class AntIdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        private readonly AntBase _ant;
        
        private AntWarriorProfessionBase _antWarriorProfessionBase;
        
        public AntIdleState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if ((_ant.CurProfessionType == ProfessionType.MeleeWarrior || _ant.CurProfessionType == ProfessionType.RangeWarrior) &&
                 _ant.CurrentProfession.TryCast(out _antWarriorProfessionBase))
            {
                _antWarriorProfessionBase.AttackProcessor.OnEnterEnemyInZone += CheckEnemiesInZone;
                CheckEnemiesInZone();
            }
        }

        public override void OnStateExit()
        {
            if (_antWarriorProfessionBase != null)
                _antWarriorProfessionBase.AttackProcessor.OnEnterEnemyInZone -= CheckEnemiesInZone;
        }

        public override void OnUpdate()
        {
            
        }

        private void CheckEnemiesInZone()
        {
            if (!_antWarriorProfessionBase.AttackProcessor.CheckEnemiesInAttackZone()) return;
            
            _ant.HandleGiveOrder(null, UnitPathType.Attack);
        }
    }
}