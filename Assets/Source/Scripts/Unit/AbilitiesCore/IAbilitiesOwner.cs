using System.Collections.Generic;

namespace BugStrategy.Unit.AbilitiesCore
{
    public interface IAbilitiesOwner : IActiveAbilitiesOwner, IPassiveAbilitiesOwner
    {
        public IReadOnlyList<IAbility> Abilities { get; }
    }
}