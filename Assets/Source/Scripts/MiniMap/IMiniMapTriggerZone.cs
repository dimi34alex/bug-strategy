using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.MiniMap
{
    public interface IMiniMapTriggerZone
    {
        public Dictionary<MiniMapIconID, IReadOnlyList<IMiniMapObject>> MiniMapObjects { get; }
        public Vector2 Scale { get; }
        
        public event Action<MiniMapIconID> OnObjectAdd;
        public event Action<MiniMapIconID> OnObjectRemove;
    }
}