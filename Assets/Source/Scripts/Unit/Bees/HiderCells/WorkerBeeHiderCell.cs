using BugStrategy.ResourcesSystem;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Bees
{
    public class WorkerBeeHiderCell : HiderCellBase
    {
        public readonly bool ResourceExtracted;
        public readonly ResourceID ExtractedResourceID;
        
        public WorkerBeeHiderCell(UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor) : base(unit)
        {
            ResourceExtracted = resourceExtractionProcessor.GotResource;
            ExtractedResourceID = resourceExtractionProcessor.ExtractedResourceID;
        }
    }
}