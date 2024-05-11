using System;

namespace Unit.ProcessorsCore
{
    public interface IReadOnlyResourceExtractionProcessor
    {
        public int ExtractionCapacity { get; }
        public ResourceID ExtractedResourceID { get; }
        public bool GotResource { get; }
        public bool IsExtract { get; }
        public ResourceSourceBase PrevResourceSource { get; }

        public event Action OnResourceExtracted;
        public event Action OnStorageResources;
    }
}