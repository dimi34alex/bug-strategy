using Unit.Professions;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntAttackState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Attack;

        private readonly AntBase _ant;

        private WarriorProfessionBase _warriorProfession;
        
        public AntAttackState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            if(_ant.CurProfessionType != ProfessionType.MeleeWarrior && _ant.CurProfessionType != ProfessionType.RangeWarrior ||
               !_ant.Profession.TryCast(out _warriorProfession) ||
               !_warriorProfession.AttackProcessor.CheckEnemiesInAttackZone())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurProfessionType} | " +
                                 $"{!_ant.Profession.TryCast(out _warriorProfession)}");           
#endif
                _ant.AutoGiveOrder(null);
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
        
        private void TryAttack() => _warriorProfession.AttackProcessor.TryAttack(_ant.CurrentPathData.Target);
        
        private void OnExitEnemyFromZone()
        {
            if(_warriorProfession.AttackProcessor.EnemiesCount <= 0)
                _ant.AutoGiveOrder(null);
        }
    }
}