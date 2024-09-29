using System;
using UnityEngine;

namespace BugStrategy
{
    [Serializable]
    public struct GridKey3
    {
        [field: SerializeField] public Vector3 Position { get; private set; }

        private const int _digits = 3;

        private GridKey3(Vector3 position) 
        { 
            Position = new Vector3(
                (float)Math.Round(position.x, _digits), 
                (float)Math.Round(position.y, _digits), 
                (float)Math.Round(position.z, _digits));
        }

        public static implicit operator GridKey3(Vector3 position)
        {
            return new GridKey3(position);
        }

        public static implicit operator Vector3(GridKey3 gridKey)
        {
            return gridKey.Position;
        }
    }
}
