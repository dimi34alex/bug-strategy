using UnityEngine;

namespace BugStrategy.Constructions
{
    public class ConstructionConfigBase : ScriptableObject
    {
        [field: SerializeField] public float WarFogViewRadius { get; private set; }
    }
}