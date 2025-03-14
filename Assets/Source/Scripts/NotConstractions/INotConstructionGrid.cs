using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.NotConstructions
{
    public interface INotConstructionGrid
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }
        public bool NotConstructionExist(Vector3 position, bool blockIgnore = true);
        public bool NotConstructionExist<TType>(Vector3 position, bool blockIgnore = true) where TType : INotConstruction;
        public NotConstructionBase GetNotConstruction(Vector3 position, bool withExtract = false);

        public Vector3 RoundPositionToGrid(Vector3 position);
    }
}