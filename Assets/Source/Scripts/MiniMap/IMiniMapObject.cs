using System;

namespace MiniMapSystem
{
    public interface IMiniMapObject
    {
        public AffiliationEnum Affiliation { get; }
        public MiniMapObjectType MiniMapObjectType { get; }
        public UnityEngine.Transform Transform { get; }
    }
}