using BugStrategy.CustomTimer;

namespace BugStrategy.Unit.AbilitiesCore
{
    public interface IAbility
    {
        public AbilityType AbilityType { get; }
        public IReadOnlyTimer Cooldown { get; }
    }
}