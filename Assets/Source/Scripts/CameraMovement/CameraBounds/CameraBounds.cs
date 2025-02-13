using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.CameraMovement
{
    public class CameraBounds : IReadOnlyCameraBounds
    {
        private readonly List<Vector3> _bounds = new() { default, default };
        
        public IReadOnlyList<Vector3> Bounds => _bounds;

        public void SetBounds(Vector3 leftDownBound, Vector3 rightUpBound)
        {
            _bounds[0] = leftDownBound;
            _bounds[1] = rightUpBound;
        }
    }
}