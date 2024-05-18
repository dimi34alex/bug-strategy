using UnityEngine;

namespace Unit.Bees.Configs
{
    [CreateAssetMenu(fileName = nameof(HornetConfig), menuName = "Configs/Units/Bees/" + nameof(HornetConfig))]
    public sealed class HornetConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: Space]
        [field: Header("Ability: verified bites")]
        [field: SerializeField] public float AbilityCooldown { get; private set; }
        [field: SerializeField] public float CriticalDamageScale { get; private set; }
    }
}