using BugStrategy.ResourcesSystem;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Butterflies
{
    public class CaterpillarLevel2HiderCell : HiderCellBase
    {
        public readonly bool ResourceExtracted;
        public readonly ResourceID ExtractedResourceID;
        
        public CaterpillarLevel2HiderCell (UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor) : base(unit)
        {
            ResourceExtracted = resourceExtractionProcessor.GotResource;
            ExtractedResourceID = resourceExtractionProcessor.ExtractedResourceID;
        }
    }
}