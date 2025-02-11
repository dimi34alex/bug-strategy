using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.CameraMovement
{
    public interface IReadOnlyCameraBounds
    {
        public IReadOnlyList<Vector3> Bounds { get; }
    }
}