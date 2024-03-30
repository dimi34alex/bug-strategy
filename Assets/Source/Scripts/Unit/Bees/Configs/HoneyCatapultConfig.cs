using UnityEngine;

namespace Unit.Bees.Configs
{
    [CreateAssetMenu(fileName = nameof(HoneyCatapultConfig), menuName = "Configs/Units/Bees/" + nameof(HoneyCatapultConfig))]
    public sealed class HoneyCatapultConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRadius { get; private set; }
        [field: Space]
        [field: Header("Ability: artillery salvo")]
        [field: SerializeField] [field: Range(1, 5)] public float ConstructionDamageScale { get; private set; }
        [field: Space]
        [field: Header("Ability: sticky projectiles")]
        [field: SerializeField] [field: Range(0, 10)] public int StickyProjectileNum { get; private set; }
    }
}