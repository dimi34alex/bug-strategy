using System;
using UnityEngine;

[Serializable]
public struct ConstructionConfiguration<TConstruction> where TConstruction : IConstruction
{
    public TConstruction ConstructionPrefab;
    public ConstructionSkinBase SkinPrefab;
    public Quaternion Rotation;

    public ConstructionConfiguration(TConstruction constructionPrefab, ConstructionSkinBase skinPrefab, Quaternion rotation) : this()
    {
        ConstructionPrefab = constructionPrefab;
        SkinPrefab = skinPrefab;
        Rotation = rotation;
    }
}
