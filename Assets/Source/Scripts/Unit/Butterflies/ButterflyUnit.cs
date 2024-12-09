using System.Collections.Generic;
using BugStrategy.Unit.AbilitiesCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.Butterflies
{
    public abstract class ButterflyUnit : UnitBase, IAbilitiesOwner
    {
        public override FractionType Fraction => FractionType.Bees;

        public abstract IReadOnlyList<IAbility> Abilities { get; }
        public abstract IReadOnlyList<IActiveAbility> ActiveAbilities { get; }
        public abstract IReadOnlyList<IPassiveAbility> PassiveAbilities { get; }
        
        public void ActivateAbility(AbilityType abilityType)
        {
            var ability = ActiveAbilities.Find(a => a.AbilityType == abilityType);
            ability?.TryActivate();
        }
    }
}