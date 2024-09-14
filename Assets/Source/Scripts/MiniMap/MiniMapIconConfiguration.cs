using System;
using UnityEngine;

namespace BugStrategy.MiniMap
{
    [Serializable]
    public struct MiniMapIconConfiguration<TMiniMapIcon> 
        where TMiniMapIcon: MiniMapIconBase
    {
        public TMiniMapIcon iconPrefab;
        public Quaternion rotation;

        public MiniMapIconConfiguration(TMiniMapIcon iconPrefab, Quaternion rotation) : this()
        {
            this.iconPrefab = iconPrefab;
            this.rotation = rotation;
        }
    }
}