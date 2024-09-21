using BugStrategy.Factory;
using Zenject;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourceFactory : ObjectsFactoryBase<int, ResourceSourceBase, ResourceSourcesConfig>
    {
        public ResourceSourceFactory(DiContainer diContainer, ResourceSourcesConfig config) 
            : base(diContainer, config, "ResourceSources") { }
    }
}