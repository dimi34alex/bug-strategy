using CustomTimer;
using UnitsHideCore;

namespace Constructions.UnitsHideConstruction.Cells.BeesHiderCells
{
    public class WaspHiderCell : HiderCellBase
    {
        public readonly Timer CooldownValue;
        
        public WaspHiderCell(UnitBase unit, CooldownProcessor cooldownProcessor) : base(unit)
        {
            CooldownValue = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
        }
        
        public override void HandleUpdate(float time)
        {
            CooldownValue.Tick(time);
        }
    }
}