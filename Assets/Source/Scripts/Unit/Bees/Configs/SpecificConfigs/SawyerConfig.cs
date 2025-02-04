using BugStrategy.Projectiles;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(SawyerConfig), menuName = "Configs/Units/Bees/" + nameof(SawyerConfig))]
    public sealed class SawyerConfig : BeeConfigBase
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
        [field: Space]
        [field: Header("Ability: raise shields")]
        [field: SerializeField] public float DamageEnterScale { get; private set; }
        [field: SerializeField] public float DamageExitScale { get; private set; }
        [field: SerializeField] public float RaiseShieldsExistTime { get; private set; }
        [field: SerializeField] public float RaiseShieldsCooldown { get; private set; }
    }
}