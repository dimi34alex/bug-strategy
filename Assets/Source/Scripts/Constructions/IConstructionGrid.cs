using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public interface IConstructionGrid
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }

        public bool IsFree(Vector3 position);
        public bool Exist(Vector3 position);
        public ConstructionBase Get(Vector3 position, bool withExtract = false);

        public Vector3 RoundPositionToGrid(Vector3 position);
        
        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;
    }
}