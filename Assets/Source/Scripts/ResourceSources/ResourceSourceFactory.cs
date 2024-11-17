using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourceFactory : FactoryWithId<int, ResourceSourceBase>
    {
        public ResourceSourceFactory(DiContainer diContainer, ResourceSourcesConfig config) 
            : base(diContainer, config, "ResourceSources") { }
    }
}