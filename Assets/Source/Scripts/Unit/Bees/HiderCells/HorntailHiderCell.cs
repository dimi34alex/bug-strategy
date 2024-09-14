using BugStrategy.CustomTimer;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Bees
{
    public class HorntailHiderCell : HiderCellBase
    {
        public readonly Timer AttackCooldown;
        public readonly Timer AbilitySwordStrikeCooldown;
        
        public HorntailHiderCell(UnitBase unit, CooldownProcessor cooldownProcessor, AbilitySwordStrike abilitySwordStrike) : base(unit)
        {
            AttackCooldown = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
            AbilitySwordStrikeCooldown = new Timer(abilitySwordStrike.Cooldown.MaxTime, abilitySwordStrike.Cooldown.CurrentTime);
        }
        
        public override void HandleUpdate(float time)
        {
            AttackCooldown.Tick(time);
            AbilitySwordStrikeCooldown.Tick(time);
        }
    }
}