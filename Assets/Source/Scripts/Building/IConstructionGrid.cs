using System.Collections.Generic;
using UnityEngine;

public interface IConstructionGrid
{
    public IReadOnlyCollection<Vector3Int> Positions { get; }
    public bool ConstructionExist(Vector3Int position);
    public bool ConstructionExist<TType>(Vector3Int position) where TType : IConstruction;
    public ConstructionBase GetConstruction(Vector3Int position);

    public Vector3 RoundPositionToGrid(Vector3 position);
}