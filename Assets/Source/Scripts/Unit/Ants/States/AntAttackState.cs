using System;
using Unit.Ants.Professions;
using Unit.OrderValidatorCore;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntAttackState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Attack;

        private readonly AntBase _ant;

        private AntWarriorProfessionBase _antWarriorProfession;
        
        public override event Action StateExecuted;
        
        public AntAttackState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            if(_ant.CurProfessionType != ProfessionType.MeleeWarrior && _ant.CurProfessionType != ProfessionType.RangeWarrior ||
               !_ant.CurrentProfession.TryCast(out _antWarriorProfession) ||
               !_antWarriorProfession.AttackProcessor.CheckEnemiesInAttackZone())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurProfessionType} | " +
                                 $"{!_ant.CurrentProfession.TryCast(out _antWarriorProfession)}");           
#endif
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _antWarriorProfession.CooldownProcessor.OnCooldownEnd += TryAttack;
            _antWarriorProfession.AttackProcessor.OnExitEnemyFromZone += OnExitEnemyFromZone;

            if(_antWarriorProfession.CanAttack) TryAttack();
        }

        public override void OnStateExit()
        {
            _antWarriorProfession.CooldownProcessor.OnCooldownEnd -= TryAttack;
            _antWarriorProfession.AttackProcessor.OnExitEnemyFromZone -= OnExitEnemyFromZone;
        }

        public override void OnUpdate()
        {
            
        }
        
        private void TryAttack() => _antWarriorProfession.AttackProcessor.TryAttack(_ant.CurrentPathData.Target);
        
        private void OnExitEnemyFromZone()
        {
            if(_antWarriorProfession.AttackProcessor.EnemiesCount <= 0)
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
        }
    }
}