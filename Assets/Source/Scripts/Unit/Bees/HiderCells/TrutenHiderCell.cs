using BugStrategy.CustomTimer;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Bees
{
    public class TrutenHiderCell : HiderCellBase
    {
        public readonly Timer CooldownValue;
        
        public TrutenHiderCell(UnitBase unit, CooldownProcessor cooldownProcessor) : base(unit)
        {
            CooldownValue = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
        }
        
        public override void HandleUpdate(float time)
        {
            CooldownValue.Tick(time);
        }
    }
}