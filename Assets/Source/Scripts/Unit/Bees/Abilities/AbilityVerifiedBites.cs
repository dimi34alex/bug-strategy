using CustomTimer;

namespace Unit.Bees
{
    public sealed class AbilityVerifiedBites
    {
        private readonly HornetAttackProcessor _attackProcessor;
        private readonly float _criticalDamageScale;
        private readonly Timer _cooldown;

        public AbilityVerifiedBites(HornetAttackProcessor hornetAttackProcessor, float reloadTime, float criticalDamageScale)
        {
            _attackProcessor = hornetAttackProcessor;
            _criticalDamageScale = criticalDamageScale;
            _cooldown = new Timer(reloadTime, reloadTime);

            _attackProcessor.SetCriticalDamageScale(_criticalDamageScale);
            _attackProcessor.OnAttack += ResetCooldown;
            
            _cooldown.OnTimerEnd += ActivateAbility;
        }

        public void HandleUpdate(float time)
            => _cooldown.Tick(time);

        public void Reset()
            => _cooldown.Tick(float.MaxValue);
        
        private void ActivateAbility()
            => _attackProcessor.SetCriticalAttack();
        
        private void ResetCooldown()
        {
            if(_cooldown.TimerIsEnd)
                _cooldown.Reset();
        }
    }
}