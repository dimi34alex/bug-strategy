using BugStrategy.CustomTimer;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Bees
{
    public class HornetHiderCell : HiderCellBase
    {
        public readonly Timer AttackCooldown;
        public readonly Timer AbilityVerifiedBitesCooldown;
        
        public HornetHiderCell(UnitBase unit, CooldownProcessor cooldownProcessor, AbilityVerifiedBites abilityVerifiedBites) : base(unit)
        {
            AttackCooldown = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
            AbilityVerifiedBitesCooldown = new Timer(abilityVerifiedBites.Cooldown.MaxTime, abilityVerifiedBites.Cooldown.CurrentTime);
        }
        
        public override void HandleUpdate(float time)
        {
            AttackCooldown.Tick(time);
            AbilityVerifiedBitesCooldown.Tick(time);
        }
    }
}