using CustomTimer;

namespace Source.Scripts.Unit.AbilitiesCore
{
    public interface IAbility
    {
        public AbilityType AbilityType { get; }
        public IReadOnlyTimer Cooldown { get; }
    }
}