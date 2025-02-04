using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    public abstract class BeeConfigBase : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float WarFogViewRadius { get; private set; }
    }
}