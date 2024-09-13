using CustomTimer;
using Source.Scripts.ResourcesSystem;
using Unit.ProcessorsCore;
using UnitsHideCore;

namespace Unit.Bees
{
    public class MurmurHiderCell : HiderCellBase
    {
        public readonly bool ResourceExtracted;
        public readonly ResourceID ExtractedResourceID;
        public readonly Timer CooldownValue;
        
        public MurmurHiderCell(UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor, CooldownProcessor cooldownProcessor) : base(unit)
        {
            ResourceExtracted = resourceExtractionProcessor.GotResource;
            ExtractedResourceID = resourceExtractionProcessor.ExtractedResourceID;
            CooldownValue = new Timer(cooldownProcessor.DefaultCapacity, cooldownProcessor.CurrentValue);
        }
        
        public override void HandleUpdate(float time)
        {
            CooldownValue.Tick(time);
        }
    }
}