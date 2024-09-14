using System.Collections.Generic;

namespace BugStrategy.Unit.AbilitiesCore
{
    public interface IPassiveAbilitiesOwner
    {
        public IReadOnlyList<IPassiveAbility> PassiveAbilities { get; }
    }
}