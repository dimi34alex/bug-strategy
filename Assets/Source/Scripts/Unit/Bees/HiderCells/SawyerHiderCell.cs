using BugStrategy.CustomTimer;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Bees
{
    public class SawyerHiderCell : HiderCellBase
    {
        public readonly Timer AttackCooldown;
        public readonly Timer AbilityRaiseShields;

        public SawyerHiderCell(UnitBase unit, CooldownProcessor cooldownProcessor, AbilityRaiseShields abilityRaiseShields) : base(unit)
        {
            AttackCooldown = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
            AbilityRaiseShields = new Timer(abilityRaiseShields.Cooldown.MaxTime, abilityRaiseShields.Cooldown.CurrentTime);
        }
        
        public override void HandleUpdate(float time)
        {
            AttackCooldown.Tick(time);
            AbilityRaiseShields.Tick(time);
        }
    }
}