using CycleFramework.Extensions;

namespace BugStrategy.Constructions.ResourceProduceConstruction
{
    public class ResourceProduceCore : ResourceProduceCoreBase
    {
        private ResourceProduceProccessInfo _resourceProduceProccessInfo;
        private float _fractionalProducedResources;

        protected override ResourceProduceProccessInfoBase _produceProccessInfo => _resourceProduceProccessInfo;

        public ResourceProduceCore(ResourceProduceProccessInfo produceProccessInfo) : base(produceProccessInfo)
        {
            _resourceProduceProccessInfo = produceProccessInfo;
        }

        protected override int CalculateProducedResourceCount(float time)
        {
            _fractionalProducedResources += time * _resourceProduceProccessInfo.ProducePerSecond;
            int wholePart = (int)_fractionalProducedResources;

            _fractionalProducedResources -= wholePart;

            return wholePart;
        }

        protected override void SetResourceProduceProccessInfoCallback(ResourceProduceProccessInfoBase produceProccessInfo)
        {
            _resourceProduceProccessInfo = produceProccessInfo.Cast<ResourceProduceProccessInfo>();
        }
    }
}
