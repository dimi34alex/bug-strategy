using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public interface IConstructionGrid
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }
        
        public bool Exist(Vector3 position, bool blockIgnore = true);
        public bool Exist<TType>(Vector3 position, bool blockIgnore = true) where TType : IConstruction;
        public ConstructionBase Get(Vector3 position, bool withExtract = false);

        public Vector3 RoundPositionToGrid(Vector3 position);
        
        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;
    }
}