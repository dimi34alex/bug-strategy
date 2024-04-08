using CustomTimer;

namespace Unit.Bees
{
    public sealed class AbilityRaiseShields
    {
        private readonly Sawyer _sawyer;
        private readonly SawyerAttackProcessor _attackProcessor;
        private readonly Timer _existsTimer;
        private readonly Timer _cooldown;
        private readonly float _damageEnterScale;
        private readonly float _damageExitScale;
        
        public AbilityRaiseShields(Sawyer sawyer, SawyerAttackProcessor attackProcessor, float existsTime, 
            float cooldown, float damageEnterScale, float damageExitScale)
        {
            _sawyer = sawyer;
            _attackProcessor = attackProcessor;
            _existsTimer = new Timer(existsTime, 0, true);
            _cooldown = new Timer(cooldown, cooldown);
            _damageEnterScale = damageEnterScale;
            _damageExitScale = damageExitScale;

            _existsTimer.OnTimerEnd += OnAbilityExistEnd;
        }

        public void HandleUpdate(float time)
        {
            _existsTimer.Tick(time);
            _cooldown.Tick(time);
        }
        
        public void ActivateAbility()
        {
            if(!_cooldown.TimerIsEnd)
                return;
            
            _existsTimer.Reset();
            
            _sawyer.SetEnterDamageScale(_damageEnterScale);
            _attackProcessor.SetExitDamageScale(_damageExitScale);
        }

        private void OnAbilityExistEnd()
        {
            _sawyer.SetEnterDamageScale(1);
            _attackProcessor.SetExitDamageScale(1);
        }
    }
}