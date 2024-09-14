using System;
using BugStrategy.CustomTimer;

namespace BugStrategy
{
    public class CooldownProcessor : IReadOnlyCooldownProcessor
    {
        public readonly float DefaultCapacity;
        private readonly Timer _cooldown;
    
        public bool IsCooldown { get; private set; }
        public float CurrentValue => _cooldown.CurrentTime;
        public float CurrentCapacity => _cooldown.MaxTime;
    
        public event Action OnCooldownEnd;

        public CooldownProcessor(float cooldownTime)
        {
            DefaultCapacity = cooldownTime;
            IsCooldown = false;
            _cooldown = new Timer(cooldownTime, cooldownTime, true);
            _cooldown.OnTimerEnd += CooldownEnd;
        }

        public void HandleUpdate(float time) => _cooldown.Tick(time);
    
        public void StartCooldown()
        {
            IsCooldown = true;
            _cooldown.Reset();
        }

        public void Reset()
        {
            IsCooldown = false;
            _cooldown.Reset(true);
            _cooldown.SetMaxValue(DefaultCapacity);
        }

        private void CooldownEnd()
        {
            IsCooldown = false;
            OnCooldownEnd?.Invoke();
        }
    
        public void SetCooldownTime(float newCooldownTime)
            => _cooldown.SetMaxValue(newCooldownTime, false, true);

        public void LoadData(float currentValue)
        {
            _cooldown.Reset();
            _cooldown.Tick(currentValue);
        }
    }
}
