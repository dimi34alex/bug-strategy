using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(HorntailConfig), menuName = "Configs/Units/Bees/" + nameof(HorntailConfig))]
    public sealed class HorntailConfig : BeeConfigBase
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRadius { get; private set; }
        [field: Space]
        [field: Header("Ability: sword strike")]
        [field: SerializeField] public float SwordStrikeDamage { get; private set; }
        [field: SerializeField] public float SwordStrikeDistanceFromCenter { get; private set; }
        [field: SerializeField] public float SwordStrikeRadius { get; private set; }
        [field: SerializeField] public float SwordStrikeCooldown { get; private set; }
        [field: SerializeField] public LayerMask SwordStrikeLayers { get; private set; }
    }
}