using System.Collections.Generic;

namespace Source.Scripts.Unit.AbilitiesCore
{
    public interface IActiveAbilitiesOwner
    {
        public IReadOnlyList<IActiveAbility> ActiveAbilities { get; }
        
        public void ActivateAbility(AbilityType abilityType);
    }
}