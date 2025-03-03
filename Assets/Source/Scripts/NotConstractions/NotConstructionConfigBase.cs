using UnityEngine;

namespace BugStrategy.NotConstructions
{
    public class NotConstructionConfigBase : ScriptableObject
    {
        [field: SerializeField] public float WarFogViewRadius { get; private set; }
    }
}