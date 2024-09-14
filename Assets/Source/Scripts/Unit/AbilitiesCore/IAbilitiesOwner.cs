using System.Collections.Generic;

namespace Source.Scripts.Unit.AbilitiesCore
{
    public interface IAbilitiesOwner : IActiveAbilitiesOwner, IPassiveAbilitiesOwner
    {
        public IReadOnlyList<IAbility> Abilities { get; }
    }
}