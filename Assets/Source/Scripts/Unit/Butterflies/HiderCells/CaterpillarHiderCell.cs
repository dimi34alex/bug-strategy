using BugStrategy.ResourcesSystem;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Butterflies
{
    public class CaterpillarHiderCell : HiderCellBase
    {
        public readonly bool ResourceExtracted;
        public readonly ResourceID ExtractedResourceID;
        
        public CaterpillarHiderCell (UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor) : base(unit)
        {
            ResourceExtracted = resourceExtractionProcessor.GotResource;
            ExtractedResourceID = resourceExtractionProcessor.ExtractedResourceID;
        }
    }
}