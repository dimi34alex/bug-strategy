using System;
using CustomTimer;

public class CooldownProcessor : IReadOnlyCooldownProcessor
{
    private readonly Timer _cooldown;
    
    public bool IsCooldown { get; private set; }
    public float CooldownCurrentValue => _cooldown.CurrentTime;
    public float CooldownMaxValue => _cooldown.MaxTime;
    
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

    public void Reset()
    {
        IsCooldown = false;
        _cooldown.Reset(true);
    }

    private void CooldownEnd()
    {
        IsCooldown = false;
        OnCooldownEnd?.Invoke();
    }

    public void SetCooldownTime(float newCooldownTime)
        => _cooldown.SetMaxValue(newCooldownTime, false, true);
}
