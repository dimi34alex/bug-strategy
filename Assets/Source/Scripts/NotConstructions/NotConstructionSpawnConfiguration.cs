using System;
using UnityEngine;

namespace BugStrategy.NotConstructions
{
    [Serializable]
    public struct NotConstructionSpawnConfiguration<TConstruction> where TConstruction : INotConstruction
    {
        [field: SerializeField] public TConstruction NotConstructionPrefab { get; private set; }
        [field: SerializeField] public NotConstructionSkinBase SkinPrefab { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public NotConstructionSpawnConfiguration(TConstruction notConstructionPrefab, NotConstructionSkinBase skinPrefab, Quaternion rotation) : this()
        {
            NotConstructionPrefab = notConstructionPrefab;
            SkinPrefab = skinPrefab;
            Rotation = rotation;
        }
    }
}
