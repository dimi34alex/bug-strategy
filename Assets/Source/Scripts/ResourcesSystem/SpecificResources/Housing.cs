using System;

namespace BugStrategy.ResourcesSystem
{
    [Serializable]
    public class Housing : ResourceBase
    {
        public Housing(ResourceConfig config, float currentValue, float capacity) : base(config, currentValue, capacity)
        {
        
        }
    
    }
}
