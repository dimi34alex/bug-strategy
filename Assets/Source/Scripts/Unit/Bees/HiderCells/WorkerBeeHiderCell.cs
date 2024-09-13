using Source.Scripts.ResourcesSystem;
using Unit.ProcessorsCore;
using UnitsHideCore;

namespace Constructions.UnitsHideConstruction.Cells.BeesHiderCells
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