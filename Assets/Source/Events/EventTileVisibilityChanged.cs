using Avastrad.EventBusFramework;
using UnityEngine;

namespace BugStrategy.Events
{
    public struct EventTileVisibilityChanged : IEvent
    {
        public readonly bool IsVisible;
        public readonly Vector3 Position;
        
        public EventTileVisibilityChanged(bool isVisible, Vector3 position)
        {
            IsVisible = isVisible;
            Position = position;
        }
    }
}