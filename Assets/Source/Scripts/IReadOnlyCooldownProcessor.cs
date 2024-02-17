using System;

public interface IReadOnlyCooldownProcessor
{
    public bool IsCooldown { get; }

    public event Action OnCooldownEnd;
}