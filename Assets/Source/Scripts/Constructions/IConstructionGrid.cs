using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public interface IConstructionGrid
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }
        public bool ConstructionExist(Vector3 position, bool blockIgnore = true);
        public bool ConstructionExist<TType>(Vector3 position, bool blockIgnore = true) where TType : IConstruction;
        public ConstructionBase GetConstruction(Vector3 position, bool withExtract = false);

        public Vector3 RoundPositionToGrid(Vector3 position);
    }
}