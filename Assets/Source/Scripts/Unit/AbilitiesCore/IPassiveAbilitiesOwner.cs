using System.Collections.Generic;

namespace Source.Scripts.Unit.AbilitiesCore
{
    public interface IPassiveAbilitiesOwner
    {
        public IReadOnlyList<IPassiveAbility> PassiveAbilities { get; }
    }
}