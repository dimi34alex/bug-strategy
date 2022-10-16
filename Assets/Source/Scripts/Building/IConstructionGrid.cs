using System.Collections.Generic;
using UnityEngine;

public interface IConstructionGrid
{
    public IReadOnlyCollection<Vector3Int> Positions { get; }
    public bool ConstructionExist(Vector3Int position, bool blockIgnore = true);
    public bool ConstructionExist<TType>(Vector3Int position, bool blockIgnore = true) where TType : IConstruction;
    public ConstructionBase GetConstruction(Vector3Int position, bool withExtract = false);

    public Vector3 RoundPositionToGrid(Vector3 position);
}