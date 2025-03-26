using System;
using UnityEngine;

namespace BugStrategy.Grids.GridsProviding
{
    public interface IGridProvider
    {
        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;
    }
}