using System;
using UnityEngine;

namespace BugStrategy.Constructions
{
    [Serializable]
    public struct ConstructionSpawnConfiguration<TConstruction> where TConstruction : IConstruction
    {
        [field: SerializeField] public TConstruction ConstructionPrefab { get; private set; }
        [field: SerializeField] public ConstructionSkinBase SkinPrefab { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public ConstructionSpawnConfiguration(TConstruction constructionPrefab, ConstructionSkinBase skinPrefab, Quaternion rotation) : this()
        {
            ConstructionPrefab = constructionPrefab;
            SkinPrefab = skinPrefab;
            Rotation = rotation;
        }
    }
}
