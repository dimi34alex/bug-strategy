using Unit.ProfessionsCore;

namespace Unit.States
{
    public class AttackState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Attack;

        private readonly MovingUnit _unit;

        private readonly WarriorProfessionBase _warriorProfession;
        
        public AttackState(MovingUnit unit, WarriorProfessionBase warriorProfession)
        {
            _unit = unit;
            _warriorProfession = warriorProfession;
        }
        
        public override void OnStateEnter()
        {
            if(!_warriorProfession.AttackProcessor.CheckEnemiesInAttackZone())
            {
                _unit.AutoGiveOrder(null);
                return;
            }
            
            _warriorProfession.Cooldown.OnCooldownEnd += TryAttack;
            _warriorProfession.AttackProcessor.OnExitEnemyFromZone += OnExitEnemyFromZone;

            if(_warriorProfession.CanAttack) TryAttack();
        }

        public override void OnStateExit()
        {
            _warriorProfession.Cooldown.OnCooldownEnd -= TryAttack;
            _warriorProfession.AttackProcessor.OnExitEnemyFromZone -= OnExitEnemyFromZone;
        }

        public override void OnUpdate()
        {
            
        }
        
        private void TryAttack() => _warriorProfession.AttackProcessor.TryAttack(_unit.CurrentPathData.Target);
        
        private void OnExitEnemyFromZone()
        {
            if(_warriorProfession.AttackProcessor.EnemiesCount <= 0)
                _unit.AutoGiveOrder(null);
        }
    }
}