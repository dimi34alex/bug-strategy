using CustomTimer;
using Source.Scripts.Unit.AbilitiesCore;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class AbilityRaiseShields : IActiveAbility
    {
        private readonly Sawyer _sawyer;
        private readonly SawyerAttackProcessor _attackProcessor;
        private readonly Timer _existsTimer;
        private readonly Timer _cooldown;
        private readonly float _damageEnterScale;
        private readonly float _damageExitScale;

        public IReadOnlyTimer Cooldown => _cooldown;
        public AbilityType AbilityType => AbilityType.RaiseShields;

        public AbilityRaiseShields(Sawyer sawyer, SawyerAttackProcessor attackProcessor, float existsTime, 
            float cooldown, float damageEnterScale, float damageExitScale)
        {
            _sawyer = sawyer;
            _attackProcessor = attackProcessor;
            _existsTimer = new Timer(existsTime, 0, true);
            _cooldown = new Timer(cooldown);
            _damageEnterScale = damageEnterScale;
            _damageExitScale = damageExitScale;

            _existsTimer.OnTimerEnd += OnAbilityExistEnd;
        }

        public void HandleUpdate(float time)
        {
            _existsTimer.Tick(time);
            _cooldown.Tick(time);
        }
        
        public void TryActivate()
        {
            Debug.Log("AFAFSDFSAD");
            
            if(!_cooldown.TimerIsEnd)
                return;
            
            _existsTimer.Reset();
            
            _sawyer.SetEnterDamageScale(_damageEnterScale);
            _attackProcessor.SetExitDamageScale(_damageExitScale);
        }

        public void Reset()
        {
            _cooldown.Reset();
            _existsTimer.Reset(true);
            OnAbilityExistEnd();
        }
        
        private void OnAbilityExistEnd()
        {
            _sawyer.SetEnterDamageScale(1);
            _attackProcessor.SetExitDamageScale(1);
        }

        public void LoadData(float currentCooldownValue)
        {
            Reset();
            _cooldown.SetCurrentTIme(currentCooldownValue);
        }
    }
}