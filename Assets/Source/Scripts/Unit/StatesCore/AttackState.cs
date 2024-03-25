using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;

namespace Unit.States
{
    public class AttackState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Attack;

        private readonly MovingUnit _unit;

        private readonly AttackProcessorBase _attackProcessor;
        private readonly IReadOnlyCooldownProcessor _cooldownProcessor;

        private bool CanAttack => !_cooldownProcessor.IsCooldown; 
        
        public AttackState(MovingUnit unit, WarriorOrderValidator warriorOrderValidator)
        {
            _unit = unit;
            _attackProcessor = warriorOrderValidator.AttackProcessor;
            _cooldownProcessor = warriorOrderValidator.Cooldown;
        }
        
        public AttackState(MovingUnit unit, AttackProcessorBase attackProcessor, IReadOnlyCooldownProcessor cooldownProcessor)
        {
            _unit = unit;
            _attackProcessor = attackProcessor;
            _cooldownProcessor = cooldownProcessor;
        }
        
        public override void OnStateEnter()
        {
            if(!_attackProcessor.CheckEnemiesInAttackZone())
            {
                _unit.AutoGiveOrder(null);
                return;
            }
            
            _cooldownProcessor.OnCooldownEnd += TryAttack;
            _attackProcessor.OnExitEnemyFromZone += OnExitEnemyFromZone;

            if(CanAttack) TryAttack();
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
            => _attackProcessor.TryAttack(_unit.CurrentPathData.Target);
        
        private void OnExitEnemyFromZone()
        {
            if(_attackProcessor.EnemiesCount <= 0)
                _unit.AutoGiveOrder(null);
        }
    }
}