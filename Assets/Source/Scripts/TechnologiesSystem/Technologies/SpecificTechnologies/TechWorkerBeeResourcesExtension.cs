using BugStrategy.TechnologiesSystem.Technologies.Configs;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public class TechWorkerBeeResourcesExtension : Technology<TechWorkerBeeResourcesExtensionConfig>
    {
        public TechWorkerBeeResourcesExtension(TechWorkerBeeResourcesExtensionConfig config) 
            : base(config) { }

        public float GetCapacityScale()
        {
            if (Researched)
                return _config.ResourcesCapacityScale;

            return 1;
        }
    }
}