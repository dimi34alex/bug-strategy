using UnityEngine;

namespace Source.Scripts.ResourcesSystem
{
    public interface IReadOnlyResource : IReadOnlyFloatStorage
    {
        public ResourceID ID { get; }
        public Sprite Icon { get; }
    }
}