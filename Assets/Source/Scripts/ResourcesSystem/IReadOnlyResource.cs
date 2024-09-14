using UnityEngine;

namespace BugStrategy.ResourcesSystem
{
    public interface IReadOnlyResource : IReadOnlyFloatStorage
    {
        public ResourceID ID { get; }
        public Sprite Icon { get; }
    }
}