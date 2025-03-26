using System;
using UnityEngine;

namespace BugStrategy.Grids.GridsProviding
{
    public abstract class GridProvider : IGridProvider, IDisposable
    {
        public abstract event Action<Vector3> OnAdd;
        public abstract event Action<Vector3> OnRemove;

        public abstract void Dispose();
    }
}