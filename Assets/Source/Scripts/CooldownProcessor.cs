using System;
using CustomTimer;

public class CooldownProcessor : IReadOnlyCooldownProcessor
{
    private readonly Timer _cooldown;
        
    public bool IsCooldown { get; private set; }

    public event Action OnCooldownEnd;

    public CooldownProcessor(float cooldownTime)
    {
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

    private void CooldownEnd()
    {
        IsCooldown = false;
        OnCooldownEnd?.Invoke();
    }
}
