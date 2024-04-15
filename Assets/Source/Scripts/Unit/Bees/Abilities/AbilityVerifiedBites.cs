using CustomTimer;

namespace Unit.Bees
{
    public sealed class AbilityVerifiedBites
    {
        private readonly HornetAttackProcessor _attackProcessor;
        private readonly float _criticalDamageScale;
        private readonly Timer _cooldown;

        public IReadOnlyTimer Cooldown => _cooldown;
        
        public AbilityVerifiedBites(HornetAttackProcessor hornetAttackProcessor, float reloadTime, float criticalDamageScale)
        {
            _attackProcessor = hornetAttackProcessor;
            _criticalDamageScale = criticalDamageScale;
            _cooldown = new Timer(reloadTime);

            _attackProcessor.SetCriticalDamageScale(_criticalDamageScale);
            _attackProcessor.Attacked += Reset;
            
            _cooldown.OnTimerEnd += ActivateAbility;
        }

        public void HandleUpdate(float time)
            => _cooldown.Tick(time);

        public void Reset()
            => _cooldown.Reset();
        
        private void ActivateAbility()
            => _attackProcessor.SetCriticalAttack();

        public void LoadData(float currentCooldownValue)
        {
            Reset();
            _cooldown.SetCurrentTIme(currentCooldownValue);
        }
    }
}