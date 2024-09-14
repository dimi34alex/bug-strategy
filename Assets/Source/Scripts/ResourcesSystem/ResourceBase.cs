using UnityEngine;

namespace BugStrategy.ResourcesSystem
{
    public class ResourceBase : FloatStorage, IReadOnlyResource
    {
        public ResourceID ID { get; }
        public Sprite Icon { get; }

        public ResourceBase(ResourceConfig config, float currentValue, float capacity):base(currentValue, capacity) 
        {
            ID = config.ID;
            Icon = config.Icon;
        }
    }
}