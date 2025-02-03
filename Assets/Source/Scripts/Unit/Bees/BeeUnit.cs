using System.Collections.Generic;
using BugStrategy.Unit.AbilitiesCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.Bees
{
    public abstract class BeeUnit : UnitBase, IAbilitiesOwner
    {
        protected abstract BeeConfigBase ConfigBase { get; }
        
        public override FractionType Fraction => FractionType.Bees;

        public abstract IReadOnlyList<IAbility> Abilities { get; }
        public abstract IReadOnlyList<IActiveAbility> ActiveAbilities { get; }
        public abstract IReadOnlyList<IPassiveAbility> PassiveAbilities { get; }

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new FloatStorage(ConfigBase.HealthPoints, ConfigBase.HealthPoints);
            VisibleWarFogZone.SetRadius(ConfigBase.WarFogViewRadius);
        }

        public void ActivateAbility(AbilityType abilityType)
        {
            var ability = ActiveAbilities.Find(a => a.AbilityType == abilityType);
            ability?.TryActivate();
        }
    }
}