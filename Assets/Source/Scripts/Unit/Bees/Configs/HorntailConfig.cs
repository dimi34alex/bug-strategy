using UnityEngine;

namespace Unit.Bees.Configs
{
    [CreateAssetMenu(fileName = nameof(HorntailConfig), menuName = "Configs/Units/Bees/" + nameof(HorntailConfig))]
    public sealed class HorntailConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRadius { get; private set; }
    }
}